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

        public int PlayerStrength { get; set; }
        public bool IsPlayerAlive { get; set; }
        public int Month { get; set; }
        public int PlotBonus { get; set; }
        public int RevolutionStrength { get; set; }
        public int Minimal { get; set; }

        private IAccount account;

        private Group[] groups;
        private News[] news;

        public Engine(IAccount account)
        {
            this.account = account;

            Initialise();
            InitialiseGroups();
            InitialiseNews();
        }

        public void Initialise()
        {
            this.account.Initialise();

            this.PlayerStrength = 4;
            this.IsPlayerAlive = true;
            this.Month = 0;
            this.PlotBonus = 0;
            this.RevolutionStrength = 10;
            this.Minimal = 0;
        }

        private void InitialiseGroups()
        {
            groups = new Group[]
            {
                new Group(GroupType.Army, 7, 6, "The ARMY", "   ARMY   "),
                new Group(GroupType.Peasants, 7, 6, "The PEASANTS", " PEASANTS "),
                new Group(GroupType.Landowners, 7, 6, "The LANDOWNERS", "LANDOWNERS"),
                new Group(GroupType.Guerillas, 0, 6, "The GUERILLAS", "GUERILLAS "),
                new Group(GroupType.Leftotans, 7, 6, "The LEFTOTANS", "LEFTOTANS "),
                new Group(GroupType.SecretPolice, 7, 6, "The SECRET POLICE", " S.POLICE "),
                new Group(GroupType.Russians, 7, 0, "The RUSSIANS", " RUSSIANS "),
                new Group(GroupType.Americans, 7, 0, "The AMERICANS", "AMERICANS "),
            };
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
    }
}
