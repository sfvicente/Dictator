using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class EndScreen : IEndScreen
    {
        private readonly IAccount account;
        private readonly IGovernmentStats governmentStats;
        private readonly IGroupStats groupStats;

        public EndScreen(IAccount account, IGovernmentStats governmentStats, IGroupStats groupStats)
        {
            this.account = account;
            this.governmentStats = governmentStats;
            this.groupStats = groupStats;
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
