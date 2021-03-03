using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class AssassinationScreen : IAssassinationScreen
    {
        private readonly IGroupStats groupStats;
        private readonly IGovernmentStats governmentStats;

        public AssassinationScreen(IGroupStats groupStats, IGovernmentStats governmentStats)
        {
            this.groupStats = groupStats;
            this.governmentStats = governmentStats;
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
