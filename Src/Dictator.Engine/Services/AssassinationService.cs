﻿using Dictator.Core.Models;

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
    private readonly IPopularityService _popularityService;
    private readonly IGovernmentService governmentService;

    public AssassinationService(
        IRandomService randomService,
        IGroupService groupService,
        IPopularityService popularityService,
        IGovernmentService governmentService)
    {
        _randomService = randomService;
        this.groupService = groupService;
        _popularityService = popularityService;
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
            _popularityService.DoesPoliceHatePlayer() ||
            _popularityService.IsPoliceUnableToProtectPlayer() ||
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
}
