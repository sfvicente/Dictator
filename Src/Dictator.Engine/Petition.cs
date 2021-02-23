using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Engine
{
    public class Petition
    {
        public int Cost { get; set; }
        public bool IsMonthlyCost { get; set; }
        public string GroupPopularityChanges { get; set; }
        public string GroupStrenghtChanges { get; set; }

        public string Text { get; set; }
        public GroupType Requester { get; set; }

        public bool HasBeenUsed { get; set; }
    }
}
