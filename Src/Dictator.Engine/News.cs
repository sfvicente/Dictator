using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class News : EventAction
    {
        public News(int cost, int monthlyCost, string groupPopularityChanges, string groupStrenghtChanges, string text)
        {
            this.Cost = cost;
            this.MonthlyCost = monthlyCost;
            this.GroupPopularityChanges = groupPopularityChanges;
            this.GroupStrenghtChanges = groupStrenghtChanges;
            this.Text = Text;
            this.HasBeenUsed = false;
        }
    }
}
