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

        /// <summary>
        ///     Attempts to assign a revolt group in a scenario of revolution.
        /// </summary>
        /// <returns><c>true</c> if one of the groups becomes a group responsible for initiating a revolution; otherwise, <c>false</c>.</returns>
        public bool TryTriggerRevoltGroup()
        {
            Random random = new Random();

            for (int guess = 0; guess < 3; guess++)     // Perform 3 tries to guess the revolt group
            {
                int number = random.Next(0, 3);
                Group[] groups = groupService.GetGroups();

                if (groups[number].Status == GroupStatus.Revolution)
                {
                    SetRevolutionaryGroup(groups[number]);  // As the group has been triggered, set the group as the current revolutionary
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Finds the groups that can be possible allies of a player in a revolution.
        /// </summary>
        /// <returns>A dictionary containing the groups that can be possible allies with their respective ids.</returns>
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
        ///     Determines if a revolution has succeeded in overthrowing the player.
        /// </summary>
        /// <returns><c>true</c> if the revolution has succeeded; otherwise, <c>false</c>.</returns>
        public bool DoesRevolutionSucceed()
        {
            IRevolution revolution = GetRevolutionState();
            Random random = new Random();
            int number = random.Next(0, 3);

            if (revolution.RevolutionStrength > revolution.PlayerStrength + number - 1)
            {
                return true;
            }

            return false;
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

        public void ApplyRevolutionCrushedEffects()
        {
            BoostAllyPopularity();
            governmentService.SetPlotBonus(governmentService.GetMonth() + 2);  // Prevent revolutions for the next two months
            groupService.ResetStatusAndAllies();
            // TODO: reset player's ally and revolution properties?
        }

        /// <summary>
        ///     Boosts the player ally's popularity, which normally happens when the player crushes the revolution.
        /// </summary>
        private void BoostAllyPopularity()
        {
            if (revolution.PlayerAlly != null)
            {
                groupService.SetPopularity(revolution.PlayerAlly.Type, 9);
            }
        }
    }
}
