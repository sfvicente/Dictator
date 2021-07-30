namespace Dictator.Core.Services
{
    public interface IGovernmentService
    {
        public void Initialise();

        /// <summary>
        ///     Advances the month number of the current game.
        /// </summary>
        public void AdvanceMonth();

        /// <summary>
        ///     Retrieves the month number of the current game.
        /// </summary>
        /// <returns>The current month number.</returns>
        public int GetMonth();

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

        public int GetLastScore();
        public void SetHighScore(int totalScore);

        /// <summary>
        ///     Retrieves the current strength of the player. The player strength is always greater or equal to zero.
        /// </summary>
        /// <returns>A number representing the player's strength.</returns>
        public int GetPlayerStrength();

        public int GetMonthlyRevolutionStrength();

        /// <summary>
        ///     Sets the level of strength of a possible revolution for the current turn.
        /// </summary>
        public void SetMonthlyRevolutionStrength();

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

        public void SetPlotBonus(int plotBonus);
    }
}
