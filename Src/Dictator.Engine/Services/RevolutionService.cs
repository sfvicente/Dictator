using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class RevolutionService : IRevolutionService
    {
        private readonly IRevolution revolution;
        private readonly IGroupService groupService;

        public RevolutionService(IRevolution revolution, IGroupService groupStats)
        {
            this.revolution = revolution;
            this.groupService = groupStats;
        }

        public void SetRevolutionaryGroup(Group revolutionaryGroup)
        {
            revolution.RevolutionaryGroup = revolutionaryGroup;
        }

        public IRevolution GetRevolutionState()
        {
            return revolution;
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
