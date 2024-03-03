using System;

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
    ///     Calculates the strength of the Ritimba republic in a scenario of war.
    /// </summary>
    /// <returns>The total strength of Ritimba in a scenario of war.</returns>
    public int CalculateRitimbaStrength();

    /// <summary>
    ///     Calculates the strength of Leftoto in a scenario of war.
    /// </summary>
    /// <returns>The total strength of Leftoto.</returns>
    public int CalculateLeftotoStrength();

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
    private readonly IGroupService groupService;
    private readonly IGovernmentService governmentService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="WarService"/> class from a <see cref="IGroupService"/> and
    ///     a <see cref="IGovernmentService"/> components.
    /// </summary>
    /// <param name="groupService">The service used to provide functionality related to the groups or factions.</param>
    /// <param name="governmentService">The service used to provide functionality related to the government settings and operations.</param>
    public WarService(IGroupService groupService, IGovernmentService governmentService)
    {
        this.groupService = groupService;
        this.governmentService = governmentService;
    }

    /// <summary>
    ///     Determines if a war between the Ritimba republic and Leftoto should happen. There is a 1/3 probability that war should happen.
    /// </summary>
    /// <returns><c>true</c> if war should happen; otherwise, <c>false</c>.</returns>
    public bool ShouldWarHappen()
    {
        Random random = new Random();
        int number = random.Next(0, 3);

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
        groupService.IncreasePopularity(GroupType.Army, 1);
        groupService.IncreasePopularity(GroupType.Peasants, 1);
        groupService.IncreasePopularity(GroupType.Landowners, 1);
        groupService.DecreasePopularity(GroupType.SecretPolice, 1);
    }

    /// <summary>
    ///     Calculates the strength of the Ritimba republic in a scenario of war.
    /// </summary>
    /// <returns>The total strength of Ritimba in a scenario of war.</returns>
    public int CalculateRitimbaStrength()
    {
        int totalStrength = 0;
        Group[] groups = groupService.GetGroups();

        // Sum the strength of the army, peasants and landowners if they have minimal popularity
        for (int i = 0; i < 3; i++)
        {
            if (groups[i].Popularity > governmentService.GetMonthlyMinimalPopularityAndStrength())
            {
                totalStrength += groups[i].Strength;
            }
        }

        Group secretPoliceGroup = groupService.GetGroupByType(GroupType.SecretPolice);

        // Add the strength of the secret police strength if they have minimal popularity
        if (secretPoliceGroup.Popularity > governmentService.GetMonthlyMinimalPopularityAndStrength())
        {
            totalStrength += secretPoliceGroup.Strength;
        }

        // Add the strength of the player to the total
        totalStrength += governmentService.GetPlayerStrength();

        return totalStrength;
    }

    /// <summary>
    ///     Calculates the strength of Leftoto in a scenario of war.
    /// </summary>
    /// <returns>The total strength of Leftoto.</returns>
    public int CalculateLeftotoStrength()
    {
        int totalStrength = 0;
        Group[] groups = groupService.GetGroups();

        // Add the strength of all groups except Russians and Americans that are equal or below the minimal popularity
        for (int i = 0; i < 6; i++)
        {
            if (groups[i].Popularity <= governmentService.GetMonthlyMinimalPopularityAndStrength())
            {
                totalStrength += groups[i].Strength;
            }
        }

        return totalStrength;
    }

    /// <summary>
    ///     Executes and calculates the outcome for the war scenario between the Ritimba and Leftoto.
    /// </summary>
    /// <param name="warStats">The stats required for the war to calculate who wins.</param>
    /// <returns><c>true</c> if Ritimba wins the war; otherwise, <c>false</c>.</returns>
    public bool ExecuteWar(WarStats warStats)
    {
        Random random = new Random();
        int number = random.Next(0, 3);
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
        Group leftotans = groupService.GetGroupByType(GroupType.Leftotans);

        if (leftotans.Popularity > governmentService.GetMonthlyMinimalPopularityAndStrength() ||
            leftotans.Strength < governmentService.GetMonthlyMinimalPopularityAndStrength())
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
            RitimbanStrength = CalculateRitimbaStrength(),
            LeftotanStrength = CalculateLeftotoStrength()
        };

        return warStats;
    }
}