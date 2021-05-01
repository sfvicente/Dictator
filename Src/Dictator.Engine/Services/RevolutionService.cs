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

        public void PunishRevolutionaries()
        {
            Group revolutionaries = revolution.RevolutionaryGroup;
            Group revolutionaryAllies = revolutionaries.Ally;

            groupStats.SetStrength(revolutionaries.Type, 0);
            groupStats.SetPopularity(revolutionaries.Type, 0);
            groupStats.SetStrength(revolutionaryAllies.Type, 0);
            groupStats.SetPopularity(revolutionaryAllies.Type, 0);
        }
    }
}
