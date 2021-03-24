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
            Requester = requester;
            Cost = cost;
            MonthlyCost = monthlyCost;
            GroupPopularityChanges = groupPopularityChanges;
            GroupStrengthChanges = groupStrenghtChanges;
            Text = text;
            HasBeenUsed = false;
        }
    }
}
