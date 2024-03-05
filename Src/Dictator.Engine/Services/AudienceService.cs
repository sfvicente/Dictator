using System;
using System.Collections.Generic;
using System.Linq;

namespace Dictator.Core.Services;

/// <summary>
///     Provides functionality related to the petitions from the army, peasants and landowners.
/// </summary>
public interface IAudienceService
{
    /// <summary>
    ///     Initialises the audience data.
    /// </summary>
    public void Initialise();

    /// <summary>
    ///     Retrieves an unused audience request from the list of audience requests and
    ///     marks it as used.
    /// </summary>
    /// <returns>An unused audience request.</returns>
    public Audience SelectRandomUnusedAudienceRequest();

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
    private readonly IAccountService accountService;
    private readonly IGroupService groupService;
    private Audience[] audiences;

    public AudienceService(
        IRandomService randomService,
        IAccountService accountService,
        IGroupService groupService)
    {
        Initialise();
        _randomService = randomService;
        this.accountService = accountService;
        this.groupService = groupService;
    }

    /// <summary>
    ///     Initialises the audience data.
    /// </summary>
    public void Initialise()
    {
        audiences = new Audience[] {
            new Audience(GroupType.Army, 0, -5/*H*/, "QJLMMMMM", "PKLMMM", "     INTRODUCE CONSCRIPTION     "),
            new Audience(GroupType.Army, 0, 0, "PMJMMMMM", "NMLMMM", " REQUISITION LAND for TRAINING  "),
            new Audience(GroupType.Army, -10/*C*/, 0, "PLNMLMLM", "NMNIMM", "   ATTACK ALL GUERILLA BASES    "),
            new Audience(GroupType.Army, -8/*E*/, 0, "PLMMIMLM", "NMNKMM", "ATTACK GUERILLA BASES in LEFTOTO"),
            new Audience(GroupType.Army, 0, 0, "QONMMIMM", "NMNMMJ", "  SACK the SECRET POLICE CHIEF  "),
            new Audience(GroupType.Army, 0, 0, "PMMMLMIO", "MMMMMM", "EXPEL RUSSIAN MILITARY ADVISORS "),
            new Audience(GroupType.Army, 0, -9/*D*/, "QMLMMMMM", "OLLLMM", " INCREASE the PAY of the TROOPS "),
            new Audience(GroupType.Army, -12/*A*/, 0, "QLLMLLMM", "PLLKLM", "  BUY MORE ARMS and AMMUNITION  "),
            new Audience(GroupType.Peasants, 0, 3/*P*/, "LONMMMMM", "LMMLMM", "   STOP ARMY SIGN-UP COERCION   "),
            new Audience(GroupType.Peasants, 0, 0, "MQIMNMMM", "MOLMMM", "INCREASE the BASIC MINIMUM WAGE "),
            new Audience(GroupType.Peasants, 0, 0, "NQOMMIMM", "NNNNMJ", " CUT the POWERS of the S.POLICE "),
            new Audience(GroupType.Peasants, 0, 0, "MPKMKMMM", "MOKMMM", "STOP LEFTOTAN IMMIGRANT WORKERS "),
            new Audience(GroupType.Peasants, -10/*C*/, -8/*E*/, "LQKMOLNM", "MNLLMM", "INTRODUCE FREE EDUCATION for ALL"),
            new Audience(GroupType.Peasants, 0, 0, "MQJMNLNM", "MPJMML", "LEGALISE the FORMATION of UNIONS"),
            new Audience(GroupType.Peasants, 0, 0, "LQKMNLMM", "MOLLMM", "  FREE their IMPRISONED LEADER  "),
            new Audience(GroupType.Peasants, 0, 6/*S*/, "MPLMMMMM", "MMMLMM", "     START a PUBLIC LOTTERY     "),
            new Audience(GroupType.Landowners, 0, 0, "KMPMMMMM", "LMMMMM", "STOP MILITARY USE of THEIR LAND "),
            new Audience(GroupType.Landowners, 0, 0, "MIQMLMLM", "MKONMM", "  LOWER the BASIC MINIMUM WAGE  "),
            new Audience(GroupType.Landowners, 10/*W*/, -5/*H*/, "MMPMNMOI", "MMNMMM", "NATIONALISE AMERICAN BUSINESSES "),
            new Audience(GroupType.Landowners, 0, 5/*R*/, "MMPMJMLM", "MNOMLM", "LEVY DUTY on ALL LEFTOTO IMPORTS"),
            new Audience(GroupType.Landowners, 0, 4/*Q*/, "NNPMMIMM", "NMNNMK", " CUT SPENDING on the S. POLICE  "),
            new Audience(GroupType.Landowners, 0, -5/*H*/, "MMQMMMMM", "MMOMMM", "  DECREASE HEAVY LAND TAXATION  "),
            new Audience(GroupType.Landowners, 0, 0, "KLPMMMMM", "LLNNMM", "RELEASE TROOPS to WORK the LAND "),
            new Audience(GroupType.Landowners, -12/*A*/, -10/*C*/, "NNPMJMON", "MMPMKM", "BUILD a LARGE IRRIGATION SYSTEM "),
        };
    }

    /// <summary>
    ///     Retrieves an unused audience request from the list of audience requests and
    ///     marks it as used.
    /// </summary>
    /// <returns>An unused audience request.</returns>
    public Audience SelectRandomUnusedAudienceRequest()
    {
        IEnumerable<Audience> unusedAudiences = GetUnusedAudiences();
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
        groupService.ApplyPopularityChange(audience.GroupPopularityChanges);
        groupService.ApplyStrengthChange(audience.GroupStrengthChanges);
        accountService.ApplyTreasuryChanges(audience.Cost, audience.MonthlyCost);
    }

    /// <summary>
    ///     Refuses the audience request, resulting in a decrease of popularity with the petitioners.
    /// </summary>
    /// <param name="audience">The audience to be accepted.</param>
    public void RefuseAudienceRequest(Audience audience)
    {
        char requesterPopularityChange = audience.GroupPopularityChanges[(int)audience.Requester];

        // Decrease the player's popularity with the petitioners
        groupService.DecreasePopularity(audience.Requester, requesterPopularityChange - 'M');
    }

    /// <summary>
    ///    Retrieves all the audiences that have not been used yet.
    /// </summary>
    /// <returns>A collection of unused audiences.</returns>
    private IEnumerable<Audience> GetUnusedAudiences()
    {
        IEnumerable<Audience> unusedAudiences = audiences.Where(x => !x.HasBeenUsed);

        if (!unusedAudiences.Any())
        {
            SetAllAudiencesAsUnused();
            unusedAudiences = ((Audience[])audiences.Clone()).AsEnumerable();
        }

        return unusedAudiences;
    }

    /// <summary>
    ///     Sets all audiences as unused.
    /// </summary>
    private void SetAllAudiencesAsUnused()
    {
        foreach (var audience in audiences)
        {
            audience.HasBeenUsed = false;
        }
    }
}
