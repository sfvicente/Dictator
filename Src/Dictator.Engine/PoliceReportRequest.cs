using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class PoliceReportRequest
    {
        public bool HasEnoughBalance { get; set; }
        public bool HasEnoughPopularityWithPolice { get; set; }
        public bool HasPoliceEnoughStrength { get; set; }
        public int PoliceStrength { get; set; }
    }
}
