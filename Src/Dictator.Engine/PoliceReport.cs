using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Dictator.Core
{
    public class PoliceReport
    {
        public int Month { get; set; }
        public ReadOnlyCollection<Group> Groups { get; set; }
        public int PlayerStrength { get; set; }
        public int MonthlyRevolutionStrength { get; set; }
    }
}
