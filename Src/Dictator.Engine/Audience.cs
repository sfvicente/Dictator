using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class Audience : EventAction
    {
        public GroupType Requester { get; set; }

        public Audience(GroupType requester, int cost, int monthlyCost, string groupPopularityChanges, string groupStrenghtChanges, string text)
        {
            this.Requester = requester;
            this.Cost = cost;
            this.MonthlyCost = monthlyCost;
            this.GroupPopularityChanges = groupPopularityChanges;
            this.GroupStrengthChanges = groupStrenghtChanges;
            this.Text = text;
            this.HasBeenUsed = false;
        }
    }
}
