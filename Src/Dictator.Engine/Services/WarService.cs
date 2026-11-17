using Dictator.Core.Models;

namespace Dictator.Core.Services;

/// <summary>
///     Provides functionality related to the execution of the war mechanic in the game.
/// </summary>
public interface IWarService
{
    /// <summary>
    ///     Determines if a war between the Ritimba republic and Leftoto should happen. There is a 1/3 probability that war should happen.
    /// </summary>
    /// <returns><c>true</c> if war should happen; otherwise, <c>false</c>.</returns>
    public bool ShouldWarHappen();

    /// <summary>
    ///     Applies the effects of a threat of war with leftoto, which results in an increase of the player's popularity amongst the army,
    ///     peasants, landowners and secret police.
    /// </summary>
    public void ApplyThreatOfWarEffects();

    /// <summary>
    ///     Executes and calculates the outcome for the war scenario between the Ritimba and Leftoto.
    /// </summary>
    /// <param name="warStats">The stats required for the war to calculate who wins.</param>
    /// <returns><c>true</c> if Ritimba wins the war; otherwise, <c>false</c>.</returns>
    public bool ExecuteWar(WarStats warStats);

    /// <summary>
    ///     Determines if there is conflict between the republic of Ritimba and Leftoto. A conflict does not exist if the player is popular
    ///     with the leftotans or leftotans are weak.
    /// </summary>
    /// <returns><c>true</c> if a conflict exist with Leftoto; otherwise, <c>false</c>.</returns>
    public bool DoesConflictExist();

    /// <summary>
    ///     Initiates an invasion of the Ritimba Republic by Leftoto, which calculates teh strength of both countries in a
    ///     scenario of war.
    /// </summary>
    /// <returns>The war statistics which are composed of the strength of each country.</returns>
    public WarStats BeginInvasion();
}

/// <summary>
///     Provides functionality related to the execution of the war mechanic in the game.
/// </summary>
public class WarService : IWarService
{
    private readonly IRandomService _randomService;
    private readonly IGroupService _groupService;
    private readonly IStatsService _statsService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="WarService"/> class from a <see cref="IRandomService"/>,
    ///     a <see cref="IGroupService"/> and a <see cref="IStatsService"/> components.
    /// </summary>
    /// <param name="randomService">The service used to generate random numbers.</param>
    /// <param name="groupService">The service used to provide functionality related to the groups or factions.</param>
    /// <param name="statsService">The service used to provide functionality related to the player and monthly stats.</param>
    public WarService(
        IRandomService randomService,
        IGroupService groupService,
        IStatsService statsService)
    {
        _randomService = randomService;
        _groupService = groupService;
        _statsService = statsService;
    }

    /// <summary>
    ///     Determines if a war between the Ritimba republic and Leftoto should happen. There is a 1/3 probability that war should happen.
    /// </summary>
    /// <returns><c>true</c> if war should happen; otherwise, <c>false</c>.</returns>
    public bool ShouldWarHappen()
    {
        int number = _randomService.Next(3);

        if (number == 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Applies the effects of a threat of war with leftoto, which results in an increase of the player's popularity amongst the army,
    ///     peasants, landowners and secret police.
    /// </summary>
    public void ApplyThreatOfWarEffects()
    {
        _groupService.IncreasePopularity(GroupType.Army, 1);
        _groupService.IncreasePopularity(GroupType.Peasants, 1);
        _groupService.IncreasePopularity(GroupType.Landowners, 1);
        _groupService.DecreasePopularity(GroupType.SecretPolice, 1);
    }

    /// <summary>
    ///     Executes and calculates the outcome for the war scenario between the Ritimba and Leftoto.
    /// </summary>
    /// <param name="warStats">The stats required for the war to calculate who wins.</param>
    /// <returns><c>true</c> if Ritimba wins the war; otherwise, <c>false</c>.</returns>
    public bool ExecuteWar(WarStats warStats)
    {
        int number = _randomService.Next(3);
        int modifiedLeftotanStrength = warStats.LeftotanStrength + number - 1;

        if (warStats.RitimbanStrength > modifiedLeftotanStrength)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Determines if there is conflict between the republic of Ritimba and Leftoto. A conflict does not exist if the player is popular
    ///     with the leftotans or leftotans are weak.
    /// </summary>
    /// <returns><c>true</c> if a conflict exist with Leftoto; otherwise, <c>false</c>.</returns>
    public bool DoesConflictExist()
    {
        Group leftotans = _groupService.GetGroupByType(GroupType.Leftotans);

        if (leftotans.Popularity > _statsService.GetMonthlyMinimalPopularityAndStrength() ||
            leftotans.Strength < _statsService.GetMonthlyMinimalPopularityAndStrength())
        {
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Initiates an invasion of the Ritimba Republic by Leftoto, which calculates teh strength of both countries in a
    ///     scenario of war.
    /// </summary>
    /// <returns>The war statistics which are composed of the strength of each country.</returns>
    public WarStats BeginInvasion()
    {
        var warStats = new WarStats
        {
            RitimbanStrength = _statsService.CalculateRitimbaStrength(),
            LeftotanStrength = _statsService.CalculateLeftotoStrength()
        };

        return warStats;
    }
}