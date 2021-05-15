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

        public void AdvanceMonth()
        {
            government.Month++;
        }

        public int GetPlayerStrength()
        {
            return government.PlayerStrength;
        }

        public void IncreasePlayerStrength(int amount)
        {
            government.PlayerStrength += amount;
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

        public void SetMonthlyRevolutionStrength(int monthlyMinimalPopularityAndStrength)
        {
            government.MonthlyMinimalPopularityAndStrength = monthlyMinimalPopularityAndStrength;
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
