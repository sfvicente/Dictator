using System;

namespace Dictator.Core.Services;

public interface IGovernmentService
{
    public void Initialise();
    public int GetMonth();
    public void AdvanceMonth();
    public void KillPlayer();
    public bool IsPlayerAlive();
    public void PurchaseHelicopter();
    public bool HasPlayerPurchasedHelicopter();
    public int GetLastScore();
    public void SetHighScore(int totalScore);
    public int GetMonthlyRevolutionStrength();
    public void SetMonthlyRevolutionStrength();
    public int GetPlotBonus();
    public void SetPlotBonus(int plotBonus);
}

public class GovernmentService : IGovernmentService
{
    private readonly IRandomService _randomService;
    private readonly IGovernment _government;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GovernmentService"/> class from a <see cref="IGovernment"/>
    ///     component.
    /// </summary>
    /// <param name="groupService">The component used to represents the state of the government of the Ritimba 
    /// republic.</param>
    public GovernmentService(
        IRandomService randomService,
        IGovernment government)
    {
        _randomService = randomService;
        _government = government;
        government.LastScore = 0;
        Initialise();
    }

    public void Initialise()
    {
        _government.IsPlayerAlive = true;
        _government.HasHelicopter = false;
        _government.Month = 0;
        _government.PlotBonus = 0;
        _government.MonthlyRevolutionStrength = 10;
    }

    /// <summary>
    ///     Retrieves the month number of the current game.
    /// </summary>
    /// <returns>The current month number.</returns>
    public int GetMonth()
    {
        return _government.Month;
    }

    /// <summary>
    ///     Advances the month number of the current game.
    /// </summary>
    public void AdvanceMonth()
    {
        _government.Month++;
    }

    /// <summary>
    ///     Purchases an helicopter to have another route to escape in case of a war or revolution event.
    /// </summary>
    public void PurchaseHelicopter()
    {
        _government.HasHelicopter = true;
    }

    /// <summary>
    ///     Determines if the player has purchased an helicopter for a possible escape.
    /// </summary>
    /// <returns><c>true</c> if the player is has purchased the helicopter; otherwise, <c>false</c>.</returns>
    public bool HasPlayerPurchasedHelicopter()
    {
        return _government.HasHelicopter;
    }

    /// <summary>
    ///     Gets the score of the last played game before the current one.
    /// </summary>
    /// <returns>The score of the last played game.</returns>
    public int GetLastScore()
    {
        return _government.LastScore;
    }

    /// <summary>
    ///     Sets the specified score as the current highest score.
    /// </summary>
    /// <param name="highScore">The score to be set as the highest score.</param>
    public void SetHighScore(int highScore)
    {
        _government.LastScore = highScore;
    }

    /// <summary>
    ///     Marks the player as dead.
    /// </summary>
    public void KillPlayer()
    {
        _government.IsPlayerAlive = false;
    }

    /// <summary>
    ///     Determines if the player is currently alive.
    /// </summary>
    /// <returns><c>true</c> if the player is alive in the current game; otherwise, <c>false</c>.</returns>
    public bool IsPlayerAlive()
    {
        return _government.IsPlayerAlive;
    }

    /// <summary>
    ///     Gets the level of strength of a possible revolution for the current turn.
    /// </summary>
    public int GetMonthlyRevolutionStrength()
    {
        return _government.MonthlyRevolutionStrength;
    }

    /// <summary>
    ///     Sets the level of strength of a possible revolution for the current turn.
    /// </summary>
    public void SetMonthlyRevolutionStrength()
    {
        _government.MonthlyRevolutionStrength = _randomService.Next(10, 13);
    }

    /// <summary>
    ///     Retrieves the current plot bonus for the game.
    /// </summary>
    /// <returns>The plot bonus number.</returns>
    public int GetPlotBonus()
    {
        return _government.PlotBonus;
    }

    /// <summary>
    ///     Sets the current plot bonus for the game.
    /// </summary>
    /// <param name="plotBonus">The plot bonus number.</param>
    public void SetPlotBonus(int plotBonus)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(plotBonus);

        _government.PlotBonus = plotBonus;
    }
}
