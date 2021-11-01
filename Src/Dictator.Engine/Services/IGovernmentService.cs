namespace Dictator.Core.Services
{
    /// <summary>
    ///     Provides functionality related to the government settings and operations.
    /// </summary>
    public interface IGovernmentService
    {
        public void Initialise();

        /// <summary>
        ///     Retrieves the month number of the current game.
        /// </summary>
        /// <returns>The current month number.</returns>
        public int GetMonth();

        /// <summary>
        ///     Advances the month number of the current game.
        /// </summary>
        public void AdvanceMonth();

        /// <summary>
        ///     Retrieves the current strength of the player. The player strength is always greater or equal to zero.
        /// </summary>
        /// <returns>A number representing the player's strength.</returns>
        public int GetPlayerStrength();

        /// <summary>
        ///     Decreases the player strength level by one. If the strength attribute is zero, no action is taken as
        ///     strength can't be negative.
        /// </summary>
        public void DecreasePlayerStrength();

        /// <summary>
        ///     Increases bodyguard, resulting in an increment of the player's strength by 2.
        /// </summary>
        public void IncreaseBodyguard();

        /// <summary>
        ///     Marks the player as dead.
        /// </summary>
        public void KillPlayer();

        /// <summary>
        ///     Determines if the player is currently alive.
        /// </summary>
        /// <returns><c>true</c> if the player is alive in the current game; otherwise, <c>false</c>.</returns>
        public bool IsPlayerAlive();

        /// <summary>
        ///     Purchases an helicopter to have another route to escape in case of a war or revolution event.
        /// </summary>
        public void PurchaseHelicopter();

        /// <summary>
        ///     Determines if the player has purchased an helicopter for a possible escape.
        /// </summary>
        /// <returns><c>true</c> if the player is has purchased the helicopter; otherwise, <c>false</c>.</returns>
        public bool HasPlayerPurchasedHelicopter();

        /// <summary>
        ///     Gets the score of the last played game before the current one.
        /// </summary>
        /// <returns>The score of the last played game.</returns>
        public int GetLastScore();

        /// <summary>
        ///     Sets the specified score as the current highest score.
        /// </summary>
        /// <param name="highScore">The score to be set as the highest score.</param>
        public void SetHighScore(int totalScore);

        /// <summary>
        ///     Gets the level of strength of a possible revolution for the current turn.
        /// </summary>
        public int GetMonthlyRevolutionStrength();

        /// <summary>
        ///     Sets the level of strength of a possible revolution for the current turn.
        /// </summary>
        public void SetMonthlyRevolutionStrength();

        /// <summary>
        ///     Gets the minimal monthly level requirement for popularity and strength which is used for a diverse
        ///     number of game logic when interacting with groups, such as requesting external financial aid or 
        ///     finding allies in a revolution.
        /// </summary>
        public int GetMonthlyMinimalPopularityAndStrength();

        /// <summary>
        ///     Sets the minimal monthly level requirement for popularity and strength which is used for a diverse
        ///     number of game logic when interacting with groups, such as requesting external financial aid or 
        ///     finding allies in a revolution.
        /// </summary>
        public void SetMonthlyMinimalPopularityAndStrength();

        /// <summary>
        ///     Retrieves the current plot bonus for the game.
        /// </summary>
        /// <returns>The plot bonus number.</returns>
        public int GetPlotBonus();

        /// <summary>
        ///     Sets the current plot bonus for the game.
        /// </summary>
        /// <param name="plotBonus">The plot bonus number.</param>
        public void SetPlotBonus(int plotBonus);
    }
}
