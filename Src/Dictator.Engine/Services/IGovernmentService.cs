using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IGovernmentService
    {
        public void Initialise();
        public void AdvanceMonth();
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

        public bool IsPlayerAlive();
        public void PurchaseHelicopter();
        public bool HasPlayerPurchasedHelicopter();
        public int GetLastScore();
        public void SetHighScore(int totalScore);
        public int GetPlayerStrength();
        public int GetMonthlyRevolutionStrength();
        public int GetPlotBonus();
        public void SetMonthlyRevolutionStrength(int monthlyRevolutionStrength);
        public int GetMonthlyMinimalPopularityAndStrength();
        public void SetMonthlyMinimalPopularityAndStrength(int monthlyMinimalPopularityAndStrength);
        public void SetPlotBonus(int plotBonus);
    }
}
