using System;
using System.Collections.Generic;
using Dictator.Core.Models;

namespace Dictator.Core.Services;

/// <summary>
///     Provides functionality related to the execution of the revolution mechanic in the game.
/// </summary>
public interface IRevolutionService
{
    /// <summary>
    ///     Attempts to assign a revolt group in a scenario of revolution.
    /// </summary>
    /// <returns><c>true</c> if one of the groups becomes a group responsible for initiating a revolution; otherwise, <c>false</c>.</returns>
    public bool TryTriggerRevoltGroup();

    /// <summary>
    ///     Gets the revolutionary group and ally that initiated the revolution.
    /// </summary>
    /// <returns>The revolutionary group, their ally and combined strength.</returns>
    public Revolutionary GetRevolutionary();

    /// <summary>
    ///     Finds the groups that can be possible allies of a player in a revolution.
    /// </summary>
    /// <returns>A dictionary containing the groups that can be possible allies with their respective ids.</returns>
    public Dictionary<int, Group> FindPossibleAllies();

    /// <summary>
    ///     Determines if a group accepts to be an ally of the player during a revolution.
    /// </summary>
    /// <param name="groupId">The id of the group to accept or refuse an alliance.</param>
    /// <returns><c>true</c> if the group accepts to be an ally; otherwise, <c>false</c>.</returns>
    public bool DoesGroupAcceptAllianceInRevolution(int groupId);

    /// <summary>
    ///     Sets the specified group as the ally of the player in a scenario of a revolution.
    /// </summary>
    /// <param name="selectedAllyGroupId">The id of the group to set as the ally of the player.</param>
    public void SetPlayerAllyForRevolution(int selectedAllyGroupId);

    /// <summary>
    ///     Determines if a revolution has succeeded in overthrowing the player.
    /// </summary>
    /// <returns><c>true</c> if the revolution has succeeded; otherwise, <c>false</c>.</returns>
    public bool DoesRevolutionSucceed();

    /// <summary>
    ///     Punishes the groups that have taken part in a revolution that has been offset by the player. It
    ///     sets the revolutionaries strength and popularity to zero.
    /// </summary>
    public void PunishRevolutionaries();

    /// <summary>
    ///     Applies the effects that result of a player crushing a revolution. It boosts ally popularity, prevents revolutions
    ///     for the next two months and resets all group status and allies from previous plots.
    /// </summary>
    public void ApplyRevolutionCrushedEffects();
}

/// <summary>
///     Provides functionality related to the execution of the revolution mechanic in the game.
/// </summary>
public class RevolutionService : IRevolutionService
{
    private readonly IRandomService _randomService;
    private readonly IRevolution revolution;
    private readonly IGroupService groupService;
    private readonly IGovernmentService governmentService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RevolutionService"/> class from a <see cref="IRevolution"/>,
    ///     a <see cref="IGroupService"/> and a <see cref="IGovernmentService"/> components.
    /// </summary>
    /// <param name="revolution">The component used to setup a revolution which sets the player and a possible ally against other groups.</param>
    /// <param name="groupService">The service used to provide functionality related to the groups or factions.</param>
    /// <param name="governmentService">The service used to provide functionality related to the government settings and operations.</param>
    public RevolutionService(
        IRandomService randomService,
        IRevolution revolution,
        IGroupService groupService,
        IGovernmentService governmentService)
    {
        _randomService = randomService;
        this.revolution = revolution;
        this.groupService = groupService;
        this.governmentService = governmentService;
    }

    /// <summary>
    ///     Attempts to assign a revolt group in a scenario of revolution.
    /// </summary>
    /// <returns><c>true</c> if one of the groups becomes a group responsible for initiating a revolution; otherwise, <c>false</c>.</returns>
    public bool TryTriggerRevoltGroup()
    {
        for (int guess = 0; guess < 3; guess++)     // Perform 3 tries to guess the revolt group
        {
            int number = _randomService.Next(3);
            Group[] groups = groupService.GetGroups();

            if (groups[number].Status == GroupStatus.Revolution)
            {
                revolution.RevolutionaryGroup = groups[number];  // As the group has been triggered, set the group as the current revolutionary
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Gets the revolutionary group and ally that initiated the revolution.
    /// </summary>
    /// <returns>The revolutionary group, their ally and combined strength.</returns>
    public Revolutionary GetRevolutionary()
    {
        Revolutionary revolutionary = new Revolutionary();
        Group revolutionaryGroup = revolution.RevolutionaryGroup;

        if(revolutionaryGroup != null)
        {
            revolutionary.RevolutionaryGroupName = revolutionaryGroup.Name;

            if (revolutionaryGroup.Ally != null)
            {
                revolutionary.RevolutionaryGroupAllyName = revolutionaryGroup.Ally.Name;
                revolutionary.CombinedStrength = revolutionaryGroup.Strength + revolutionaryGroup.Ally.Strength;
            }
        }

        return revolutionary;
    }

    /// <summary>
    ///     Finds the groups that can be possible allies of a player in a revolution.
    /// </summary>
    /// <returns>A dictionary containing the groups that can be possible allies with their respective ids.</returns>
    public Dictionary<int, Group> FindPossibleAllies()
    {
        Group[] groups = groupService.GetGroups();
        Dictionary<int, Group> possibleAllies = new Dictionary<int, Group>();

        for (int i = 0; i < 6; i++)
        {
            if (groups[i].Popularity > governmentService.GetMonthlyMinimalPopularityAndStrength())
            {
                possibleAllies.Add(i + 1, groups[i]);
            }
        }

        return possibleAllies;
    }

    /// <summary>
    ///     Determines if a group accepts to be an ally of the player during a revolution.
    /// </summary>
    /// <param name="groupId">The id of the group to accept or refuse an alliance.</param>
    /// <returns><c>true</c> if the group accepts to be an ally; otherwise, <c>false</c>.</returns>
    public bool DoesGroupAcceptAllianceInRevolution(int groupId)
    {
        Group group = groupService.GetGroups()[groupId - 1];

        if (group.Popularity <= governmentService.GetMonthlyMinimalPopularityAndStrength())
        {
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Sets the specified group as the ally of the player in a scenario of a revolution.
    /// </summary>
    /// <param name="selectedAllyGroupId">The id of the group to set as the ally of the player.</param>
    public void SetPlayerAllyForRevolution(int selectedAllyGroupId)
    {
        Group group = groupService.GetGroupById(selectedAllyGroupId);

        revolution.PlayerAlly = group;
    }

    /// <summary>
    ///     Determines if a revolution has succeeded in overthrowing the player.
    /// </summary>
    /// <returns><c>true</c> if the revolution has succeeded; otherwise, <c>false</c>.</returns>
    public bool DoesRevolutionSucceed()
    {
        int number = _randomService.Next(3);

        if (revolution.RevolutionStrength > revolution.PlayerStrength + number - 1)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Punishes the groups that have taken part in a revolution that has been offset by the player. It
    ///     sets the revolutionaries strength and popularity to zero.
    /// </summary>
    public void PunishRevolutionaries()
    {
        Group revolutionaries = revolution.RevolutionaryGroup;
        Group revolutionaryAllies = revolutionaries.Ally;

        groupService.SetStrength(revolutionaries.Type, 0);
        groupService.SetPopularity(revolutionaries.Type, 0);
        groupService.SetStrength(revolutionaryAllies.Type, 0);
        groupService.SetPopularity(revolutionaryAllies.Type, 0);
    }

    /// <summary>
    ///     Applies the effects that result of a player crushing a revolution. It boosts ally popularity, prevents revolutions
    ///     for the next two months and resets all group status and allies from previous plots.
    /// </summary>
    public void ApplyRevolutionCrushedEffects()
    {
        BoostAllyPopularity();
        governmentService.SetPlotBonus(governmentService.GetMonth() + 2);  // Prevent revolutions for the next two months
        groupService.ResetStatusAndAllies();
        // TODO: reset player's ally and revolution properties?
    }

    /// <summary>
    ///     Boosts the player ally's popularity, which normally happens when the player crushes the revolution.
    /// </summary>
    private void BoostAllyPopularity()
    {
        if (revolution.PlayerAlly != null)
        {
            groupService.SetPopularity(revolution.PlayerAlly.Type, 9);
        }
    }
}
