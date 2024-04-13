using Dictator.Core.Models;

namespace Dictator.Core.Services;

/// <summary>
///     Provides functionality related to the player assassination mechanic.
/// </summary>
public interface IAssassinationService
{
    /// <summary>
    ///     Determines if an assassination attempt on the player should happen by one of the following groups: army, 
    ///     peasants, landowners and guerrilas.
    /// </summary>
    /// <returns><c>true</c> if an assassination attempt should happen; otherwise, <c>false</c>.</returns>
    bool ShouldAssassinationAttemptHappen();

    /// <summary>
    ///     Determines if an assassination attempt on the player is successful.
    /// </summary>
    /// <returns><c>true</c> if the assassination atempt is successful; otherwise, <c>false</c>.</returns>
    bool IsAssassinationSuccessful();
    string GetAssassinationGroupName(GroupType assassinGroupType);
}

/// <summary>
///     Provides functionality related to the player assassination mechanic.
/// </summary>
public class AssassinationService : IAssassinationService
{
    private readonly IRandomService _randomService;
    private readonly IGroupService _groupService;
    private readonly IStatsService _statsService;

    private GroupType _assassinGroupType;
    public GroupType AssassinGroupType { get { return _assassinGroupType; } }

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
    ///     Determines if an assassination attempt on the player should happen by one of the following groups: army, 
    ///     peasants, landowners and guerrilas.
    /// </summary>
    /// <returns><c>true</c> if an assassination attempt should happen; otherwise, <c>false</c>.</returns>
    public bool ShouldAssassinationAttemptHappen()
    {
        int number = _randomService.Next(3);
        Group[] groups = _groupService.GetGroups();

        // Select a random group between the army, peasants, landowners and guerrilas
        if (groups[number].Status == GroupStatus.Assassination)
        {
            SetAssassinByGroupType(groups[number].Type);
            return true;
        }

        return false;
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

    public void SetAssassinByGroupType(GroupType groupType)
    {
        _assassinGroupType = groupType;
    }
}
