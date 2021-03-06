using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IGovernmentStats
    {
        public int PlayerStrength { get; set; }
        public bool IsPlayerAlive { get; }
        public int Month { get; set; }
        public int PlotBonus { get; set; }
        public int MonthlyRevolutionStrength { get; set; }
        public int MonthlyMinimalPopularityAndStrength { get; set; }

        public void Initialise();
        public void DecreasePlayerStrength();
        public void KillPlayer();
    }
}
