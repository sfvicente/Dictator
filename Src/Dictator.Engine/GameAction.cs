namespace Dictator.Core
{
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
