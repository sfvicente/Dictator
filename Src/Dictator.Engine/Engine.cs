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
        private News[] news;

        public Engine(IAccount account, IGovernmentStats governmentStats, IGroupStats groupStats)
        {
            this.account = account;
            this.governmentStats = governmentStats;
            this.groupStats = groupStats;

            Initialise();
            InitialiseNews();
        }

        public void Initialise()
        {
            this.account.Initialise();
            this.governmentStats.Initialise();
            this.groupStats.Initialise();
        }

        private void InitialiseNews()
        {
            news = new News[]
            {
                new News(0, 0, "MMMMMIMM", "MMMQMI", " PRESIDENT LOSES S.POLICE FILES "),
                new News(0, 0, "MMMMMMMM", "LMMVMM", " CUBANS ARM and TRAIN GUERILLAS "),
                new News(0, 0, "MMMMMMMM", "IMMOMN", "ACCIDENT. ARMY BARRACKS BLOWS UP"),
                new News(0, 0, "MMMMMMMM", "MMJMKM", "   BANANA PRICES FALL by 98%    "),
                new News(0, 0, "MMMMMMMM", "MMOMIM", "  MAJOR EARTHQUAKE in LEFTOTO   "),
                new News(0, 0, "MMMMMMMM", "MILKMM", "A PLAGUE SWEEPS through PEASANTS"),
            };
        }

        public void AdvanceMonth()
        {
            this.governmentStats.Month++;
        }
    }
}
