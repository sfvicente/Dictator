namespace Dictator.Core
{
    /// <summary>
    ///     Represents the state of the government of the Ritimba republic.
    /// </summary>
    public interface IGovernment
    {
        /// <summary>
        ///     Gets or sets a value indicating whether the player is currently alive.
        /// </summary>
        public bool IsPlayerAlive { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the player has acquired an helicopter.
        /// </summary>
        public bool HasHelicopter { get; set; }

        /// <summary>
        ///     Gets or sets the player strength. 
        /// </summary>
        public int PlayerStrength { get; set; }

        /// <summary>
        ///     Gets or sets the current month of government. 
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        ///     Gets or sets the plot bonus. 
        /// </summary>
        public int PlotBonus { get; set; }

        /// <summary>
        ///     Gets or sets the monthly revolution strength. 
        /// </summary>
        public int MonthlyRevolutionStrength { get; set; }

        /// <summary>
        ///     Gets or sets the current high score. 
        /// </summary>
        public int LastScore { get; set; }
    }
}
