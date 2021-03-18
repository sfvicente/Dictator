using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IGovernmentStats
    {
        bool IsPlayerAlive { get; }
        bool HasHelicopter { get; }
        int PlayerStrength { get; }
        int Month { get; }
        int PlotBonus { get; set; }
        int MonthlyRevolutionStrength { get; set; }
        int MonthlyMinimalPopularityAndStrength { get; set; }

        void Initialise();
        void AdvanceMonth();
        void DecreasePlayerStrength();
        void KillPlayer();
    }
}
