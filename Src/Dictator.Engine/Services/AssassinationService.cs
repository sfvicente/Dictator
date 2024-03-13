using System;
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
    private readonly IGroupService groupService;
    private readonly IGovernmentService governmentService;

    public AssassinationService(
        IRandomService randomService,
        IGroupService groupService,
        IGovernmentService governmentService)
    {
        _randomService = randomService;
        this.groupService = groupService;
        this.governmentService = governmentService;
    }

    /// <summary>
    ///     Determines if an assassination attempt on the player is successful.
    /// </summary>
    /// <returns><c>true</c> if the assassination atempt is successful; otherwise, <c>false</c>.</returns>
    public bool IsAssassinationSuccessful()
    {
        int number = _randomService.Next(0, 2);

        if (groupService.DoesMainPopulationHatePlayer() ||
            DoesPoliceHatePlayer() ||
            IsPoliceUnableToProtectPlayer() ||
            number == 0) // Player is just unlucky

        {
            return true;
        }

        return false;
    }

    public string GetAssassinationGroupName(GroupType assassinGroupType)
    {
        string groupName = groupService.GetGroupByType(assassinGroupType).Name;

        return groupName;
    }

    /// <summary>
    ///     Determines if the popularity with the secret police is less or equal to the minimum required monthly popularity.
    /// </summary>
    /// <returns><c>true</c> if the player is not popular enough with the secret police; otherwise, <c>false</c>.</returns>
    private bool DoesPoliceHatePlayer()
    {
        Group police = groupService.GetGroupByType(GroupType.SecretPolice);

        if (police.Popularity <= governmentService.GetMonthlyMinimalPopularityAndStrength())
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Determines if the police strength is less or equal than the minimum required monthly strength.
    /// </summary>
    /// <returns><c>true</c> if the police is not strong enough to protect the player; otherwise, <c>false</c>.</returns>
    private bool IsPoliceUnableToProtectPlayer()
    {
        Group police = groupService.GetGroupByType(GroupType.SecretPolice);

        if (police.Strength <= governmentService.GetMonthlyMinimalPopularityAndStrength())
        {
            return true;
        }

        return false;
    }
}
