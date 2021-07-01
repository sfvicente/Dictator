using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class WarService : IWarService
    {
        private readonly IGroupService groupService;
        private readonly IGovernmentService governmentService;

        public WarService(IGroupService groupService, IGovernmentService governmentService)
        {
            this.groupService = groupService;
            this.governmentService = governmentService;
        }

        /// <summary>
        ///     Determines if a war between the Ritimba republic and Leftoto should happen. There is a 1/3 probability that war should happen.
        /// </summary>
        /// <returns><c>true</c> if war should happen; otherwise, <c>false</c>.</returns>
        public bool ShouldWarHappen()
        {
            Random random = new Random();
            int number = random.Next(0, 3);

            if (number == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Applies the effects of a threat of war with leftoto, which results in an increase of the player's popularity amongst the army,
        ///     peasants, landowners and secret police.
        /// </summary>
        public void ApplyThreatOfWarEffects()
        {
            groupService.IncreasePopularity(GroupType.Army, 1);
            groupService.IncreasePopularity(GroupType.Peasants, 1);
            groupService.IncreasePopularity(GroupType.Landowners, 1);
            groupService.DecreasePopularity(GroupType.SecretPolice, 1);
        }

        /// <summary>
        ///     Calculates the strength of the Ritimba republic in a scenario of war.
        /// </summary>
        /// <returns>The total strength of Ritimba in a scenario of war.</returns>
        public int CalculateRitimbaStrength()
        {
            int totalStrength = 0;
            Group[] groups = groupService.GetGroups();

            // Sum the strength of the army, peasants and landowners if they have minimal popularity
            for (int i = 0; i < 3; i++)
            {
                if (groups[i].Popularity > governmentService.GetMonthlyMinimalPopularityAndStrength())
                {
                    totalStrength += groups[i].Strength;
                }
            }

            Group secretPoliceGroup = groupService.GetGroupByType(GroupType.SecretPolice);

            // Add the strength of the secret police strength if they have minimal popularity
            if (secretPoliceGroup.Popularity > governmentService.GetMonthlyMinimalPopularityAndStrength())
            {
                totalStrength += secretPoliceGroup.Strength;
            }

            // Add the strength of the player to the total
            totalStrength += governmentService.GetPlayerStrength();

            return totalStrength;
        }

        /// <summary>
        ///     Calculates the strength of Leftoto in a scenario of war.
        /// </summary>
        /// <returns>The total strength of Leftoto.</returns>
        public int CalculateLeftotoStrength()
        {
            int totalStrength = 0;
            Group[] groups = groupService.GetGroups();

            // Add the strength of all groups except Russians and Americans that are equal or below the minimal popularity
            for (int i = 0; i < 6; i++)
            {
                if (groups[i].Popularity <= governmentService.GetMonthlyMinimalPopularityAndStrength())
                {
                    totalStrength += groups[i].Strength;
                }
            }

            return totalStrength;
        }

        /// <summary>
        ///     Executes and calculates the outcome for the war scenario between the Ritimba and Leftoto.
        /// </summary>
        /// <param name="warStats">The stats required for the war to calculate who wins.</param>
        /// <returns><c>true</c> if Ritimba wins the war; otherwise, <c>false</c>.</returns>
        public bool ExecuteWar(WarStats warStats)
        {
            Random random = new Random();
            int number = random.Next(0, 3);
            int modifiedLeftotanStrength = warStats.LeftotanStrength + number - 1;

            if (warStats.RitimbanStrength > modifiedLeftotanStrength)
            {
                return true;
            }

            return false;
        }
    }
}
