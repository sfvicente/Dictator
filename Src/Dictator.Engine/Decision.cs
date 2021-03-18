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
            this.Type = type;
            this.Cost = cost;
            this.MonthlyCost = monthlyCost;
            this.GroupPopularityChanges = groupPopularityChanges;
            this.GroupStrengthChanges = groupStrenghtChanges;
            this.Text = text;
            this.HasBeenUsed = false;
        }
    }
}
