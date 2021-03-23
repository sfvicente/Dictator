using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class Decision: EventAction
    {
        public DecisionType Type { get; set; }

        public Decision(DecisionType type, int cost, int monthlyCost, string groupPopularityChanges, string groupStrenghtChanges, string text)
        {
            Type = type;
            Cost = cost;
            MonthlyCost = monthlyCost;
            GroupPopularityChanges = groupPopularityChanges;
            GroupStrengthChanges = groupStrenghtChanges;
            Text = text;
            HasBeenUsed = false;
        }
    }
}
