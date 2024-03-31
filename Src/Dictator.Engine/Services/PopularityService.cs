using Dictator.Core.Models;

namespace Dictator.Core.Services;

public interface IPopularityService
{
    /// <summary>
    ///     Gets or sets the monthly minimal popularity and strength requirement.. 
    /// </summary>
    int MonthlyMinimalPopularityAndStrength { get; set; }

    void Initialise();
    int GetMonthlyMinimalPopularityAndStrength();
    void SetMonthlyMinimalPopularityAndStrength();
    bool DoesPoliceHatePlayer();
    bool IsPoliceUnableToProtectPlayer();
}

public class PopularityService
{
    private readonly IRandomService _randomService;
    private readonly IGroupService _groupService;

    public int MonthlyMinimalPopularityAndStrength { get; set; }

    public PopularityService(IRandomService randomService, IGroupService groupService)
    {
        Initialise();
        _randomService = randomService;
        _groupService = groupService;
    }

    public void Initialise()
    {
        MonthlyMinimalPopularityAndStrength = 0;
    }

    /// <summary>
    ///     Gets the minimal monthly level requirement for popularity and strength which is used for a diverse
    ///     number of game logic when interacting with groups, such as requesting external financial aid or 
    ///     finding allies in a revolution.
    /// </summary>
    int GetMonthlyMinimalPopularityAndStrength()
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
}
