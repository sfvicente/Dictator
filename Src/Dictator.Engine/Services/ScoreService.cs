using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IGroupService groupService;
        private readonly IGovernmentService governmentService;
        private readonly IAccountService accountService;

        public ScoreService(IGroupService groupService, IGovernmentService governmentService, IAccountService accountService)
        {
            this.groupService = groupService;
            this.governmentService = governmentService;
            this.accountService = accountService;
        }

        public Score GetCurrentScore()
        {
            int totalPopularity = groupService.GetTotalPopularity();
            int monthsInOffice = governmentService.GetMonth();
            int pointsForMonthsInOffice = monthsInOffice * 3;
            int moneyGrabbed = accountService.GetSwissBankAccountBalance();
            int pointsForMoneyGrabbing = moneyGrabbed / 10;
            int highestScore = governmentService.GetLastScore();
            int pointsForStayingAlive = governmentService.IsPlayerAlive() ? 10 : 0;
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

            return score;
        }

        public void SaveHighScore()
        {
            Score score = GetCurrentScore();
            int highestScore = GetCurrentHighscore();

            if(score.TotalScore > highestScore)
            {
                governmentService.SetHighScore(score.TotalScore);
            }
        }

        public int GetCurrentHighscore()
        {
            int highestScore = governmentService.GetLastScore();

            return highestScore;
        }
    }
}
