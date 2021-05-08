using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class RevolutionService : IRevolutionService
    {
        private readonly IRevolution revolution;
        private readonly IGroupService groupStats;

        public RevolutionService(IRevolution revolution, IGroupService groupStats)
        {
            this.revolution = revolution;
            this.groupStats = groupStats;
        }

        public void SetRevolutionaryGroup(Group revolutionaryGroup)
        {
            revolution.RevolutionaryGroup = revolutionaryGroup;
        }

        public void PunishRevolutionaries()
        {
            Group revolutionaries = revolution.RevolutionaryGroup;
            Group revolutionaryAllies = revolutionaries.Ally;

            groupStats.SetStrength(revolutionaries.Type, 0);
            groupStats.SetPopularity(revolutionaries.Type, 0);
            groupStats.SetStrength(revolutionaryAllies.Type, 0);
            groupStats.SetPopularity(revolutionaryAllies.Type, 0);
        }

        /// <summary>
        ///     Boosts the player ally's popularity, which normally happens when the player crushes the revolution.
        /// </summary>
        public void BoostAllyPopularity()
        {
            if(revolution.PlayerAlly != null)
            {
                groupStats.SetPopularity(revolution.PlayerAlly.Type, 9);
            }
        }
    }
}
