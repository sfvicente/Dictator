using Dictator.Core.Models;

namespace Dictator.Core.Services;

/// <summary>
///     Provides functionality related to the player assassination mechanic.
/// </summary>
public interface IAssassinationService
{
    /// <summary>
    ///     Determines if an assassination attempt on the player is successful.
    /// </summary>
    /// <returns><c>true</c> if the assassination atempt is successful; otherwise, <c>false</c>.</returns>
    public bool IsAssassinationSuccessful();
    public string GetAssassinationGroupName(GroupType assassinGroupType);
}

/// <summary>
///     Provides functionality related to the player assassination mechanic.
/// </summary>
public class AssassinationService : IAssassinationService
{
    private readonly IRandomService _randomService;
    private readonly IGroupService _groupService;
    private readonly IStatsService _statsService;

    public AssassinationService(
        IRandomService randomService,
        IGroupService groupService,
        IStatsService statsService)
    {
        _randomService = randomService;
        _groupService = groupService;
        _statsService = statsService;
    }

    /// <summary>
    ///     Determines if an assassination attempt on the player is successful.
    /// </summary>
    /// <returns><c>true</c> if the assassination atempt is successful; otherwise, <c>false</c>.</returns>
    public bool IsAssassinationSuccessful()
    {
        int number = _randomService.Next(0, 2);

        if (_groupService.DoesMainPopulationHatePlayer() ||
            _statsService.DoesPoliceHatePlayer() ||
            _statsService.IsPoliceUnableToProtectPlayer() ||
            number == 0) // Player is just unlucky

        {
            return true;
        }

        return false;
    }

    public string GetAssassinationGroupName(GroupType assassinGroupType)
    {
        string groupName = _groupService.GetGroupByType(assassinGroupType).Name;

        return groupName;
    }
}
