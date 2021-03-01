using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class Engine : IEngine
    {
        //public int TreasuryBalance { get; set; }
        //public int MonthlyCosts { get; set; }
        //public int SwissBankAccountBalance { get; set; }

        //public int PlayerStrength { get; set; }
        //public bool IsPlayerAlive { get; set; }
        //public int Month { get; set; }
        //public int PlotBonus { get; set; }
        //public int RevolutionStrength { get; set; }
        //public int Minimal { get; set; }

        private IAccount account;
        private readonly IGovernmentStats governmentStats;
        private readonly IGroupStats groupStats;
        private readonly INewsStats newsStats;

        public Engine(
            IAccount account,
            IGovernmentStats governmentStats,
            IGroupStats groupStats,
            INewsStats newsStats)
        {
            this.account = account;
            this.governmentStats = governmentStats;
            this.groupStats = groupStats;
            this.newsStats = newsStats;
            Initialise();
        }

        public void Initialise()
        {
            this.account.Initialise();
            this.governmentStats.Initialise();
            this.groupStats.Initialise();
            newsStats.Initialise();
        }

        public void AdvanceMonth()
        {
            this.governmentStats.Month++;
        }

        public bool IsGovernmentBankrupt()
        {
            return account.TreasuryBalance <= 0;
        }

        public void SetMonthlyMinimalPopularityAndStrength()
        {
            Random random = new Random();

            governmentStats.MonthlyMinimalPopularityAndStrength = random.Next(2, 4);
        }

        public void SetMonthlyRevolutionStrength()
        {
            Random random = new Random();

            governmentStats.MonthlyRevolutionStrength = random.Next(10, 13);
        }
    }
}
