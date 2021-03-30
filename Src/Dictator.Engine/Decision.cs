using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class Decision: EventAction
    {
        public DecisionType Type { get; set; }
        public DecisionSubType DecisionSubType { get; set; }

        public Decision(
            DecisionType type,
            DecisionSubType decisionSubType,
            int cost,
            int monthlyCost,
            string groupPopularityChanges,
            string groupStrenghtChanges,
            string text)
        {
            Type = type;
            DecisionSubType = decisionSubType;
            Cost = cost;
            MonthlyCost = monthlyCost;
            GroupPopularityChanges = groupPopularityChanges;
            GroupStrengthChanges = groupStrenghtChanges;
            Text = text;
            HasBeenUsed = false;
        }
    }
}
