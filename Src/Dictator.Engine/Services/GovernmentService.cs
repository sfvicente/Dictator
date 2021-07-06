﻿using System;
using System.Collections.Generic;
using System.Text;

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

        public void DecreasePlayerStrength()
        {
            if (government.PlayerStrength > 0)
            {
                government.PlayerStrength--;
            }
        }

        public void PurchaseHelicopter()
        {
            government.HasHelicopter = true;
        }

        public bool HasPlayerPurchasedHelicopter()
        {
            return government.HasHelicopter;
        }

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

        public bool IsPlayerAlive()
        {
            return government.IsPlayerAlive;
        }

        public int GetMonthlyRevolutionStrength()
        {
            return government.MonthlyRevolutionStrength;
        }
        
        public int GetMonthlyMinimalPopularityAndStrength()
        {
            return government.MonthlyMinimalPopularityAndStrength;
        }

        public void SetMonthlyRevolutionStrength(int monthlyRevolutionStrength)
        {
            government.MonthlyRevolutionStrength = monthlyRevolutionStrength;
        }

        public int GetPlotBonus()
        {
            return government.PlotBonus;
        }

        public void SetMonthlyMinimalPopularityAndStrength(int monthlyMinimalPopularityAndStrength)
        {
            government.MonthlyMinimalPopularityAndStrength = monthlyMinimalPopularityAndStrength;
        }

        public void SetPlotBonus(int plotBonus)
        {
            government.PlotBonus = plotBonus;
        }
    }
}
