using System;

namespace Dictator.Core.Services
{
    public class GovernmentService : IGovernmentService
    {
        private readonly IGovernment government;

        public GovernmentService(IGovernment government)
        {
            this.government = government;
            government.LastScore = 0;
            Initialise();
        }

        public void Initialise()
        {
            government.IsPlayerAlive = true;
            government.HasHelicopter = false;
            government.PlayerStrength = 4;
            government.Month = 0;
            government.PlotBonus = 0;
            government.MonthlyRevolutionStrength = 10;
            government.MonthlyMinimalPopularityAndStrength = 0;
        }

        /// <summary>
        ///     Retrieves the month number of the current game.
        /// </summary>
        /// <returns>The current month number.</returns>
        public int GetMonth()
        {
            return government.Month;
        }

        /// <summary>
        ///     Advances the month number of the current game.
        /// </summary>
        public void AdvanceMonth()
        {
            government.Month++;
        }

        /// <summary>
        ///     Retrieves the current strength of the player. The player strength is always greater or equal to zero.
        /// </summary>
        /// <returns>A number representing the player's strength.</returns>
        public int GetPlayerStrength()
        {
            return government.PlayerStrength;
        }

        /// <summary>
        ///     Increases bodyguard, resulting in an increment of the player's strength by 2.
        /// </summary>
        public void IncreaseBodyguard()
        {
            government.PlayerStrength += 2;
        }

        /// <summary>
        ///     Decreases the player strength level by one. If the strength attribute is zero, no action is taken as
        ///     strength can't be negative.
        /// </summary>
        public void DecreasePlayerStrength()
        {
            if (government.PlayerStrength > 0)
            {
                government.PlayerStrength--;
            }
        }

        /// <summary>
        ///     Purchases an helicopter to have another route to escape in case of a war or revolution event.
        /// </summary>
        public void PurchaseHelicopter()
        {
            government.HasHelicopter = true;
        }

        /// <summary>
        ///     Determines if the player has purchased an helicopter for a possible escape.
        /// </summary>
        /// <returns><c>true</c> if the player is has purchased the helicopter; otherwise, <c>false</c>.</returns>
        public bool HasPlayerPurchasedHelicopter()
        {
            return government.HasHelicopter;
        }

        /// <summary>
        ///     Gets the score of the last played game before the current one.
        /// </summary>
        /// <returns>The score of the last played game.</returns>
        public int GetLastScore()
        {
            return government.LastScore;
        }

        public void SetHighScore(int highScore)
        {
            government.LastScore = highScore;
        }

        /// <summary>
        ///     Marks the player as dead.
        /// </summary>
        public void KillPlayer()
        {
            government.IsPlayerAlive = false;
        }

        /// <summary>
        ///     Determines if the player is currently alive.
        /// </summary>
        /// <returns><c>true</c> if the player is alive in the current game; otherwise, <c>false</c>.</returns>
        public bool IsPlayerAlive()
        {
            return government.IsPlayerAlive;
        }

        /// <summary>
        ///     Gets the level of strength of a possible revolution for the current turn.
        /// </summary>
        public int GetMonthlyRevolutionStrength()
        {
            return government.MonthlyRevolutionStrength;
        }

        /// <summary>
        ///     Sets the level of strength of a possible revolution for the current turn.
        /// </summary>
        public void SetMonthlyRevolutionStrength()
        {
            Random random = new Random();

            government.MonthlyRevolutionStrength = random.Next(10, 13);
        }

        /// <summary>
        ///     Gets the minimal monthly level requirement for popularity and strength which is used for a diverse
        ///     number of game logic when interacting with groups, such as requesting external financial aid or 
        ///     finding allies in a revolution.
        /// </summary>
        public int GetMonthlyMinimalPopularityAndStrength()
        {
            return government.MonthlyMinimalPopularityAndStrength;
        }

        /// <summary>
        ///     Sets the minimal monthly level requirement for popularity and strength which is used for a diverse
        ///     number of game logic when interacting with groups, such as requesting external financial aid or 
        ///     finding allies in a revolution.
        /// </summary>
        public void SetMonthlyMinimalPopularityAndStrength()
        {
            Random random = new Random();

            government.MonthlyMinimalPopularityAndStrength = random.Next(2, 5);
        }

        /// <summary>
        ///     Retrieves the current plot bonus for the game.
        /// </summary>
        /// <returns>The plot bonus number.</returns>
        public int GetPlotBonus()
        {
            return government.PlotBonus;
        }

        /// <summary>
        ///     Sets the current plot bonus for the game.
        /// </summary>
        /// <param name="plotBonus">The plot bonus number.</param>
        public void SetPlotBonus(int plotBonus)
        {
            if(plotBonus < 0)
            {
                throw new ArgumentException($"Argument '{nameof(plotBonus)}' must be greater or equal to zero: {plotBonus}");
            }

            government.PlotBonus = plotBonus;
        }
    }
}
