using Dictator.Core.Models;

namespace Dictator.Core.Services;

public interface IScoreService
{
    public Score GetCurrentScore();

    /// <summary>
    ///     Retrieves the currently highest achieved saved score.
    /// </summary>
    /// <returns>The current high score.</returns>
    public int GetCurrentHighScore();

    /// <summary>
    ///     Records the current score as the new high score if it is greater than the previous score.
    /// </summary>
    public void SaveHighScore();
}

public class ScoreService : IScoreService
{
    private readonly IGroupService _groupService;
    private readonly IGovernmentService _governmentService;
    private readonly IAccountService _accountService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ScoreService"/> class from a <see cref="IAccountService"/>,
    ///     a <see cref="IGroupService"/> and a <see cref="IGovernmentService"/> components.
    /// </summary>
    /// <param name="accountService">The service used to provide functionality related to the treasury and associated costs and
    /// the Swiss bank account.</param>
    /// <param name="groupService">The service used to provide functionality related to the groups or factions.</param>
    /// <param name="governmentService">The service used to provide functionality related to the government settings and operations.</param>
    public ScoreService(IAccountService accountService, IGroupService groupService, IGovernmentService governmentService)
    {
        _groupService = groupService;
        _governmentService = governmentService;
        _accountService = accountService;
    }

    /// <summary>
    ///     Retrieves the score for the player based on the current game state.
    /// </summary>
    /// <returns>The current score.</returns>
    public Score GetCurrentScore()
    {
        int totalPopularity = _groupService.GetTotalPopularity();
        int monthsInOffice = _governmentService.GetMonth();
        int pointsForMonthsInOffice = monthsInOffice * 3;
        int moneyGrabbed = _accountService.GetSwissBankAccountBalance();
        int pointsForMoneyGrabbing = moneyGrabbed / 10;
        int highestScore = _governmentService.GetLastScore();
        int pointsForStayingAlive = _governmentService.IsPlayerAlive() ? 10 : 0;
        int totalScore = totalPopularity + pointsForMonthsInOffice + pointsForMoneyGrabbing + pointsForStayingAlive;

        Score score = new()
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
    ///     Records the current score as the new high score if it is greater than the previous score.
    /// </summary>
    public void SaveHighScore()
    {
        Score score = GetCurrentScore();
        int highestScore = GetCurrentHighScore();

        if(score.TotalScore > highestScore)
        {
            _governmentService.SetHighScore(score.TotalScore);
        }
    }

    /// <summary>
    ///     Retrieves the currently highest achieved saved score.
    /// </summary>
    /// <returns>The current high score.</returns>
    public int GetCurrentHighScore()
    {
        int highestScore = _governmentService.GetLastScore();

        return highestScore;
    }
}
