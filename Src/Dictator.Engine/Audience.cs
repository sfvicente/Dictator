namespace Dictator.Core
{
    public class Audience : GameAction
    {
        public GroupType Requester { get; set; }

        public bool NoMoneyInvolved
        {
            get
            {
                return Cost == 0 && MonthlyCost == 0;
            }
        }

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
