using System;

namespace Dictator.Core.Services
{
    /// <summary>
    ///     Provides functionality related to the player assassination mechanic.
    /// </summary>
    public class AssassinationService : IAssassinationService
    {
        private readonly IGroupService groupService;
        private readonly IGovernmentService governmentService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AssassinationService"/> class from a <see cref="IGroupService"/> and
        ///     a <see cref="IGovernmentService"/> components.
        /// </summary>
        /// <param name="groupService">The service used to provide functionality related to the groups or factions.</param>
        /// <param name="governmentService">The service used to provide functionality related to the government settings and operations.</param>
        public AssassinationService(IGroupService groupService, IGovernmentService governmentService)
        {
            this.groupService = groupService;
            this.governmentService = governmentService;
        }

        /// <summary>
        ///     Determines if an assassination attempt on the player is successful.
        /// </summary>
        /// <returns><c>true</c> if the assassination atempt is successful; otherwise, <c>false</c>.</returns>
        public bool IsAssassinationSuccessful()
        {
            Random random = new Random();
            int number = random.Next(0, 2);

            if (groupService.DoesMainPopulationHatePlayer() ||
                DoesPoliceHatePlayer() ||
                IsPoliceUnableToProtectPlayer() ||
                number == 0) // Player is just unlucky

            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Retrieves the name of the group responsible for the assassination attempt.
        /// </summary>
        /// <returns>The name of the assassination group.</returns>
        public string GetAssassinationGroupName()
        {
            GroupType assassinGroupType = groupService.AssassinGroupType;
            string groupName = groupService.GetGroupByType(assassinGroupType).Name;

            return groupName;
        }

        /// <summary>
        ///     Determines if the popularity with the secret police is less or equal to the minimum required monthly popularity.
        /// </summary>
        /// <returns><c>true</c> if the player is not popular enough with the secret police; otherwise, <c>false</c>.</returns>
        private bool DoesPoliceHatePlayer()
        {
            Group police = groupService.GetGroupByType(GroupType.SecretPolice);

            if (police.Popularity <= governmentService.GetMonthlyMinimalPopularityAndStrength())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Determines if the police strength is less or equal than the minimum required monthly strength.
        /// </summary>
        /// <returns><c>true</c> if the police is not strong enough to protect the player; otherwise, <c>false</c>.</returns>
        private bool IsPoliceUnableToProtectPlayer()
        {
            Group police = groupService.GetGroupByType(GroupType.SecretPolice);

            if (police.Strength <= governmentService.GetMonthlyMinimalPopularityAndStrength())
            {
                return true;
            }

            return false;
        }
    }
}
