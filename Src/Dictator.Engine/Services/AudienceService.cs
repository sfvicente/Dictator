using System;
using System.Collections.Generic;
using System.Linq;
using Dictator.Core.Models;

namespace Dictator.Core.Services;

/// <summary>
///     Provides functionality related to the petitions from the army, peasants and landowners.
/// </summary>
public interface IAudienceService
{
    /// <summary>
    ///     Retrieves an unused audience request from the list of audience requests and
    ///     marks it as used.
    /// </summary>
    /// <returns>An unused audience request.</returns>
    public Audience SelectRandomUnusedAudienceRequest(Audience[] audiences);

    /// <summary>
    ///     Accepts an audience request with the associated modifications to group popularity and strength and also the changes to the treasury.
    /// </summary>
    /// <param name="audience">The audience to be accepted.</param>
    public void AcceptAudienceRequest(Audience audience);

    /// <summary>
    ///     Refuses the audience request, resulting in a decrease of popularity with the petitioners.
    /// </summary>
    /// <param name="audience">The audience to be accepted.</param>
    public void RefuseAudienceRequest(Audience audience);
}

/// <summary>
///     Provides functionality related to the petitions from the army, peasants and landowners.
/// </summary>
public class AudienceService : IAudienceService
{
    private readonly IRandomService _randomService;
    private readonly IAccountService _accountService;
    private readonly IGroupService _groupService;

    public AudienceService(
        IRandomService randomService,
        IAccountService accountService,
        IGroupService groupService)
    {
        _randomService = randomService;
        _accountService = accountService;
        _groupService = groupService;
    }

    /// <summary>
    ///     Retrieves an unused audience request from the list of audience requests and
    ///     marks it as used.
    /// </summary>
    /// <returns>An unused audience request.</returns>
    public Audience SelectRandomUnusedAudienceRequest(Audience[] audiences)
    {
        IEnumerable<Audience> unusedAudiences = GetUnusedAudiences(audiences);
        var randomUnusedAudience = unusedAudiences.ElementAt(_randomService.Next(unusedAudiences.Count()));

        randomUnusedAudience.HasBeenUsed = true;

        return randomUnusedAudience;
    }

    /// <summary>
    ///     Accepts an audience request with the associated modifications to group popularity and strength and also the changes to the treasury.
    /// </summary>
    /// <param name="audience">The audience to be accepted.</param>
    public void AcceptAudienceRequest(Audience audience)
    {
        _groupService.ApplyPopularityChange(audience.GroupPopularityChanges);
        _groupService.ApplyStrengthChange(audience.GroupStrengthChanges);
        _accountService.ApplyTreasuryChanges(audience.Cost, audience.MonthlyCost);
    }

    /// <summary>
    ///     Refuses the audience request, resulting in a decrease of popularity with the petitioners.
    /// </summary>
    /// <param name="audience">The audience to be accepted.</param>
    public void RefuseAudienceRequest(Audience audience)
    {
        char requesterPopularityChange = audience.GroupPopularityChanges[(int)audience.Requester];

        // Decrease the player's popularity with the petitioners
        _groupService.DecreasePopularity(audience.Requester, requesterPopularityChange - 'M');
    }

    /// <summary>
    ///    Retrieves all the audiences that have not been used yet.
    /// </summary>
    /// <returns>A collection of unused audiences.</returns>
    private IEnumerable<Audience> GetUnusedAudiences(Audience[] audiences)
    {
        IEnumerable<Audience> unusedAudiences = audiences.Where(x => !x.HasBeenUsed);

        if (!unusedAudiences.Any())
        {
            // Set all audiences as unused
            audiences.ToList().ForEach(a => a.HasBeenUsed = false);
            unusedAudiences = ((Audience[])audiences.Clone()).AsEnumerable();
        }

        return unusedAudiences;
    }
}
