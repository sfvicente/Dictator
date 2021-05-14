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

        public RevolutionService(IRevolution revolution, IGroupService groupStats, IGovernmentService governmentService)
        {
            this.revolution = revolution;
            this.groupService = groupStats;
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
                if (groups[i].Popularity > governmentService.MonthlyMinimalPopularityAndStrength)
                {
                    possibleAllies.Add(i, groups[i]);
                }
            }

            return possibleAllies;
        }

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
