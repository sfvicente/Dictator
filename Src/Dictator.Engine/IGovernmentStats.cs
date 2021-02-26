using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IGovernmentStats
    {
        public int PlayerStrength { get; set; }
        public bool IsPlayerAlive { get; set; }
        public int Month { get; set; }
        public int PlotBonus { get; set; }
        public int RevolutionStrength { get; set; }
        public int Minimal { get; set; }

        public void Initialise();
    }
}
