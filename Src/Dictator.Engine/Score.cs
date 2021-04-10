using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class Score
    {
        public int TotalPopularity { get; set; }
        public int MonthsInOffice { get; set; }
        public int PointsForMonthsInOffice { get; set; }
        public int PointsForStayingAlive { get; set; }
        public int MoneyGrabbed { get; set; }
        public int PointsForMoneyGrabbing { get; set; }
        public int TotalScore { get; set; }
        public int HighestScore { get; set; }
    }
}
