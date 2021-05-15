using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IGovernment
    {
        public bool IsPlayerAlive { get; set; }
        public bool HasHelicopter { get; set; }
        public int PlayerStrength { get; set; }
        public int Month { get; set; }
        public int PlotBonus { get; set; }
        public int MonthlyRevolutionStrength { get; set; }
        public int MonthlyMinimalPopularityAndStrength { get; set; }
        public int LastScore { get; set; }
    }
}
