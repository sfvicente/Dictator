using System;
using System.Collections.Generic;
using System.Text;

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

        public bool HasPlayerPurchasedHelicopter();
        public int GetLastScore();
        public void SetHighScore(int totalScore);
        public int GetPlayerStrength();
        public int GetMonthlyRevolutionStrength();
        public int GetPlotBonus();

        /// <summary>
        ///     Sets the level of strength of a possible revolution for the current turn.
        /// </summary>
        public void SetMonthlyRevolutionStrength(int monthlyRevolutionStrength);

        public int GetMonthlyMinimalPopularityAndStrength();
        public void SetMonthlyMinimalPopularityAndStrength();
        public void SetPlotBonus(int plotBonus);
    }
}
