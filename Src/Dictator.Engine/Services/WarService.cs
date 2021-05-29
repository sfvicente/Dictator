using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class WarService : IWarService
    {
        private readonly IGroupService groupService;

        public WarService(IGroupService groupService)
        {
            this.groupService = groupService;
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
    }
}
