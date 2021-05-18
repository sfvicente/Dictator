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

        /// <summary>
        ///     Retrieves the score for the player based on the current game state.
        /// </summary>
        /// <returns>The current score.</returns>
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

        /// <summary>
        ///     Records the current score as the new high-score if it is greater than the previous score.
        /// </summary>
        public void SaveHighScore()
        {
            Score score = GetCurrentScore();
            int highestScore = GetCurrentHighscore();

            if(score.TotalScore > highestScore)
            {
                governmentService.SetHighScore(score.TotalScore);
            }
        }

        /// <summary>
        ///     Retrieves the current highest score.
        /// </summary>
        /// <returns></returns>
        public int GetCurrentHighscore()
        {
            int highestScore = governmentService.GetLastScore();

            return highestScore;
        }
    }
}
