using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IGovernmentService
    {
        public bool IsPlayerAlive { get; }
        public bool HasHelicopter { get; }
        public int PlayerStrength { get; }
        public int Month { get; }
        public int PlotBonus { get; set; }
        public int MonthlyRevolutionStrength { get; set; }
        public int MonthlyMinimalPopularityAndStrength { get; set; }
        public int LastScore { get; set; }

        public void Initialise();
        public void AdvanceMonth();
        public void IncreasePlayerStrength(int amount);
        public void DecreasePlayerStrength();
        public void KillPlayer();
        void PurchaseHelicopter();
    }
}
