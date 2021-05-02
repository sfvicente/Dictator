using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class RevolutionService : IRevolutionService
    {
        private readonly IRevolution revolution;
        private readonly IGroupStats groupStats;

        public RevolutionService(IRevolution revolution, IGroupStats groupStats)
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

        public void ApplyRevolutionCrushedEffects()
        {
            if(revolution.PlayerAlly != null)
            {
                groupStats.SetPopularity(revolution.PlayerAlly.Type, 9);
            }

            // TODO: set plot bonus

            groupStats.ResetStatusAndAllies();

            // TODO: reset player's ally and revolution properties?
        }
    }
}
