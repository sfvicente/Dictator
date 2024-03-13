using Dictator.Core.Models;

namespace Dictator.Core
{
    /// <summary>
    ///     Represents a petition by one of the groups that request the player to perform an action.
    /// </summary>
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
