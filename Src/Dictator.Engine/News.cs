using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class News : GameAction
    {
        public News(int cost, int monthlyCost, string groupPopularityChanges, string groupStrenghtChanges, string text)
        {
            Cost = cost;
            MonthlyCost = monthlyCost;
            GroupPopularityChanges = groupPopularityChanges;
            GroupStrengthChanges = groupStrenghtChanges;
            Text = text;
            HasBeenUsed = false;
        }
    }
}
