namespace Dictator.Core.Models
{
    /// <summary>
    ///     Represents a news event in the game that happens outside of the player control.
    /// </summary>
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
