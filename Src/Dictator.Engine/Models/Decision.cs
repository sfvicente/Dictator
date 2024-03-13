namespace Dictator.Core.Models
{
    /// <summary>
    ///     Represents a decision selected by the player with the corresponding effects on the treasury and
    ///     the groups popularity and strength.
    /// </summary>
    public class Decision : GameAction
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
