using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class PoliceReport
    {
        public int TreasuryBalance { get; set; }
        public int MonthlyCosts { get; set; }
        public int Month { get; set; }
        public int PlayerStrength { get; set; }
        public int RevolutionStrength { get; set; }
        public int Minimal { get; set; }

        public Group[] Groups { get; set; }
    }
}
