namespace Dictator.Core.Models
{
    /// <summary>
    ///     Represents an action that is performed during an instance of a game that might affect individual
    ///     groups and treasury.
    /// </summary>
    public abstract class GameAction
    {
        public int Cost { get; set; }
        public int MonthlyCost { get; set; }
        public string GroupPopularityChanges { get; set; }
        public string GroupStrengthChanges { get; set; }
        public string Text { get; set; }
        public bool HasBeenUsed { get; set; }
    }
}
