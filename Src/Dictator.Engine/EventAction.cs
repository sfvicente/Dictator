using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Engine
{
    public abstract class EventAction
    {
        public int Cost { get; set; }
        public int MonthlyCost { get; set; }
        public string GroupPopularityChanges { get; set; }
        public string GroupStrenghtChanges { get; set; }
        public string Text { get; set; }
        public bool HasBeenUsed { get; set; }
    }
}
