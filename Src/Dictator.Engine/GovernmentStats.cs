using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class GovernmentStats : IGovernmentStats
    {
        public int PlayerStrength { get; set; }
        public bool IsPlayerAlive { get; set; }
        public int Month { get; set; }
        public int PlotBonus { get; set; }
        public int MonthlyRevolutionStrength { get; set; }
        public int MonthlyMinimalPopularityAndStrength { get; set; }

        public GovernmentStats()
        {
            this.Initialise();
        }

        public void Initialise()
        {
            this.PlayerStrength = 4;
            this.IsPlayerAlive = true;
            this.Month = 0;
            this.PlotBonus = 0;
            this.MonthlyRevolutionStrength = 10;
            this.MonthlyMinimalPopularityAndStrength = 0;
        }

        public void DecreasePlayerStrength()
        {
            if (PlayerStrength > 0)
            {
                PlayerStrength--;
            }
        }
    }
}
