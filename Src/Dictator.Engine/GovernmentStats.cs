using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class GovernmentStats : IGovernmentStats
    {
        public bool IsPlayerAlive { get; private set; }
        public bool HasHelicopter { get; private set; }
        public int PlayerStrength { get; private set; }
        public int Month { get; private set; }
        public int PlotBonus { get; set; }
        public int MonthlyRevolutionStrength { get; set; }
        public int MonthlyMinimalPopularityAndStrength { get; set; }
        public int LastScore { get; set; }

        public GovernmentStats()
        {
            Initialise();
            LastScore = 0;
        }

        public void Initialise()
        {
            IsPlayerAlive = true;
            HasHelicopter = false;
            PlayerStrength = 4;
            Month = 0;
            PlotBonus = 0;
            MonthlyRevolutionStrength = 10;
            MonthlyMinimalPopularityAndStrength = 0;
        }

        public void AdvanceMonth()
        {
            Month++;
        }

        public void IncreasePlayerStrength(int amount)
        {
            PlayerStrength += amount;
        }

        public void DecreasePlayerStrength()
        {
            if (PlayerStrength > 0)
            {
                PlayerStrength--;
            }
        }

        public void PurchaseHelicopter()
        {
            HasHelicopter = true;
        }

        public void KillPlayer()
        {
            IsPlayerAlive = false;
        }
    }
}
