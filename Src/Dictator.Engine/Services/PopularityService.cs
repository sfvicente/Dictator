using Dictator.Core.Models;

namespace Dictator.Core.Services;

public interface IStatsService
{
    /// <summary>
    ///     Gets or sets the monthly minimal popularity and strength requirement.. 
    /// </summary>
    int MonthlyMinimalPopularityAndStrength { get; set; }

    /// <summary>
    ///     Gets or sets the player strength. 
    /// </summary>
    public int PlayerStrength { get; set; }

    void Initialise();
    int GetMonthlyMinimalPopularityAndStrength();
    void SetMonthlyMinimalPopularityAndStrength();
    bool DoesPoliceHatePlayer();
    bool IsPoliceUnableToProtectPlayer();

    public int GetPlayerStrength();
    public void IncreaseBodyguard();
    public void DecreasePlayerStrength();

    /// <summary>
    ///     Calculates the strength of Leftoto in a scenario of war.
    /// </summary>
    /// <returns>The total strength of Leftoto.</returns>
    public int CalculateLeftotoStrength();

    /// <summary>
    ///     Calculates the strength of the Ritimba republic in a scenario of war.
    /// </summary>
    /// <returns>The total strength of Ritimba in a scenario of war.</returns>
    public int CalculateRitimbaStrength();
}

public class StatsService : IStatsService
{
    private readonly IRandomService _randomService;
    private readonly IGroupService _groupService;

    public int MonthlyMinimalPopularityAndStrength { get; set; }
    public int PlayerStrength { get; set; }

    public StatsService(IRandomService randomService, IGroupService groupService)
    {
        Initialise();
        _randomService = randomService;
        _groupService = groupService;
    }

    public void Initialise()
    {
        MonthlyMinimalPopularityAndStrength = 0;
        PlayerStrength = 4;
    }

    /// <summary>
    ///     Gets the minimal monthly level requirement for popularity and strength which is used for a diverse
    ///     number of game logic when interacting with groups, such as requesting external financial aid or 
    ///     finding allies in a revolution.
    /// </summary>
    public int GetMonthlyMinimalPopularityAndStrength()
    {
        return MonthlyMinimalPopularityAndStrength;
    }

    /// <summary>
    ///     Sets the minimal monthly level requirement for popularity and strength which is used for a diverse
    ///     number of game logic when interacting with groups, such as requesting external financial aid or 
    ///     finding allies in a revolution.
    /// </summary>
    public void SetMonthlyMinimalPopularityAndStrength()
    {
        MonthlyMinimalPopularityAndStrength = _randomService.Next(2, 5);
    }

    /// <summary>
    ///     Determines if the popularity with the secret police is less or equal to the minimum required monthly popularity.
    /// </summary>
    /// <returns><c>true</c> if the player is not popular enough with the secret police; otherwise, <c>false</c>.</returns>
    public bool DoesPoliceHatePlayer()
    {
        Group police = _groupService.GetGroupByType(GroupType.SecretPolice);

        if (police.Popularity <= MonthlyMinimalPopularityAndStrength)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Determines if the police strength is less or equal than the minimum required monthly strength.
    /// </summary>
    /// <returns><c>true</c> if the police is not strong enough to protect the player; otherwise, <c>false</c>.</returns>
    public bool IsPoliceUnableToProtectPlayer()
    {
        Group police = _groupService.GetGroupByType(GroupType.SecretPolice);

        if (police.Strength <= MonthlyMinimalPopularityAndStrength)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Retrieves the current strength of the player. The player strength is always greater or equal to zero.
    /// </summary>
    /// <returns>A number representing the player's strength.</returns>
    public int GetPlayerStrength()
    {
        return PlayerStrength;
    }

    /// <summary>
    ///     Increases bodyguard, resulting in an increment of the player's strength by 2.
    /// </summary>
    public void IncreaseBodyguard()
    {
        PlayerStrength += 2;
    }

    /// <summary>
    ///     Decreases the player strength level by one. If the strength attribute is zero, no action is taken as
    ///     strength can't be negative.
    /// </summary>
    public void DecreasePlayerStrength()
    {
        if (PlayerStrength > 0)
        {
            PlayerStrength--;
        }
    }

    /// <summary>
    ///     Calculates the strength of the Ritimba republic in a scenario of war.
    /// </summary>
    /// <returns>The total strength of Ritimba in a scenario of war.</returns>
    public int CalculateRitimbaStrength()
    {
        int totalStrength = 0;
        Group[] groups = _groupService.GetGroups();

        // Sum the strength of the army, peasants and landowners if they have minimal popularity
        for (int i = 0; i < 3; i++)
        {
            if (groups[i].Popularity > MonthlyMinimalPopularityAndStrength)
            {
                totalStrength += groups[i].Strength;
            }
        }

        Group secretPoliceGroup = _groupService.GetGroupByType(GroupType.SecretPolice);

        // Add the strength of the secret police strength if they have minimal popularity
        if (secretPoliceGroup.Popularity > MonthlyMinimalPopularityAndStrength)
        {
            totalStrength += secretPoliceGroup.Strength;
        }

        // Add the strength of the player to the total
        totalStrength += PlayerStrength;

        return totalStrength;
    }

    /// <summary>
    ///     Calculates the strength of Leftoto in a scenario of war.
    /// </summary>
    /// <returns>The total strength of Leftoto.</returns>
    public int CalculateLeftotoStrength()
    {
        int totalStrength = 0;
        Group[] groups = _groupService.GetGroups();

        // Add the strength of all groups except Russians and Americans that are equal or below the minimal popularity
        for (int i = 0; i < 6; i++)
        {
            if (groups[i].Popularity <= MonthlyMinimalPopularityAndStrength)
            {
                totalStrength += groups[i].Strength;
            }
        }

        return totalStrength;
    }
}
