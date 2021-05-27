using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class RevolutionService : IRevolutionService
    {
        private readonly IRevolution revolution;
        private readonly IGroupService groupService;
        private readonly IGovernmentService governmentService;

        public RevolutionService(IRevolution revolution, IGroupService groupService, IGovernmentService governmentService)
        {
            this.revolution = revolution;
            this.groupService = groupService;
            this.governmentService = governmentService;
        }

        public void SetRevolutionaryGroup(Group revolutionaryGroup)
        {
            revolution.RevolutionaryGroup = revolutionaryGroup;
        }

        public IRevolution GetRevolutionState()
        {
            return revolution;
        }

        public Dictionary<int, Group> FindPossibleAllies()
        {
            Group[] groups = groupService.GetGroups();
            Dictionary<int, Group> possibleAllies = new Dictionary<int, Group>();

            for (int i = 0; i < 6; i++)
            {
                if (groups[i].Popularity > governmentService.GetMonthlyMinimalPopularityAndStrength())
                {
                    possibleAllies.Add(i + 1, groups[i]);
                }
            }

            return possibleAllies;
        }

        /// <summary>
        ///     Determines if a group accepts to be an ally of the player during a revolution.
        /// </summary>
        /// <param name="groupId">The id of the group to accept or refuse an alliance.</param>
        /// <returns><c>true</c> if the group accepts to be an ally; otherwise, <c>false</c>.</returns>
        public bool DoesGroupAcceptAllianceInRevolution(int groupId)
        {
            Group group = groupService.GetGroups()[groupId - 1];

            if (group.Popularity <= governmentService.GetMonthlyMinimalPopularityAndStrength())
            {
                return false;
            }

            return true;
        }

        public void SetPlayerAllyForRevolution(int selectedAllyGroupId)
        {
            Group group = groupService.GetGroupById(selectedAllyGroupId);

            revolution.PlayerAlly = group;
        }

        /// <summary>
        ///     Punishes the groups that have taken part in a revolution that has been offset by the player. It
        ///     sets the revolutionaries strength and popularity to zero.
        /// </summary>
        public void PunishRevolutionaries()
        {
            Group revolutionaries = revolution.RevolutionaryGroup;
            Group revolutionaryAllies = revolutionaries.Ally;

            groupService.SetStrength(revolutionaries.Type, 0);
            groupService.SetPopularity(revolutionaries.Type, 0);
            groupService.SetStrength(revolutionaryAllies.Type, 0);
            groupService.SetPopularity(revolutionaryAllies.Type, 0);
        }

        /// <summary>
        ///     Boosts the player ally's popularity, which normally happens when the player crushes the revolution.
        /// </summary>
        public void BoostAllyPopularity()
        {
            if(revolution.PlayerAlly != null)
            {
                groupService.SetPopularity(revolution.PlayerAlly.Type, 9);
            }
        }
    }
}
