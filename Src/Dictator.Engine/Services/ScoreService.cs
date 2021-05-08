﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IGroupStats groupStats;
        private readonly IGovernmentService governmentStats;
        private readonly IAccountService accountService;

        public ScoreService(IGroupStats groupStats, IGovernmentService governmentStats, IAccountService accountService)
        {
            this.groupStats = groupStats;
            this.governmentStats = governmentStats;
            this.accountService = accountService;
        }

        public Score GetCurrentScore()
        {
            int totalPopularity = groupStats.GetTotalPopularity();
            int monthsInOffice = governmentStats.Month;
            int pointsForMonthsInOffice = monthsInOffice * 3;
            int moneyGrabbed = accountService.GetSwissBankAccountBalance();
            int pointsForMoneyGrabbing = moneyGrabbed / 10;
            int highestScore = governmentStats.LastScore;
            int pointsForStayingAlive = governmentStats.IsPlayerAlive ? 10 : 0;
            int totalScore = totalPopularity + pointsForMonthsInOffice + pointsForMoneyGrabbing + pointsForStayingAlive;

            Score score = new Score()
            {
                TotalPopularity = totalPopularity,
                MonthsInOffice = monthsInOffice,
                PointsForMonthsInOffice = pointsForMonthsInOffice,
                MoneyGrabbed = moneyGrabbed,
                PointsForMoneyGrabbing = pointsForMoneyGrabbing,
                HighestScore = highestScore,
                PointsForStayingAlive = pointsForStayingAlive,
                TotalScore = totalScore
            };

            // TODO: save history?

            return score;
        }

        public void SaveHighScore()
        {
            Score score = GetCurrentScore();
            int highestScore = GetCurrentHighscore();

            if(score.TotalScore > highestScore)
            {
                governmentStats.LastScore = score.TotalScore;                
            }
        }

        public int GetCurrentHighscore()
        {
            int highestScore = governmentStats.LastScore;

            return highestScore;
        }
    }
}
