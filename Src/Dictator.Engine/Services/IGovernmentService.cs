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
        public void IncreasePlayerStrength(int amount);
        public void DecreasePlayerStrength();
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
