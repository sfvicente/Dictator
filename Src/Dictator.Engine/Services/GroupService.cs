using System;
using System.ComponentModel;
using System.Linq;
using Dictator.Core.Models;

namespace Dictator.Core.Services;

/// <summary>
///     Provides functionality related to the groups or factions that exist within the game context.
/// </summary>
public interface IGroupService
{
    public void Initialise();
    public Group[] GetGroups();

    public Group GetGroupById(int groupId);
    public int GetPopularityByGroupType(GroupType groupType);
    public int GetStrengthByGroupType(GroupType groupType);
    public GroupType GetGroupTypeByCountry(LenderCountry country);
    public Group GetGroupByType(GroupType groupType);
    public string GetGroupNameByIndex(int index);
    public void SetPopularity(GroupType groupType, int popularity);
    public void IncreasePopularity(GroupType groupType, int amount);
    public void DecreasePopularity(GroupType groupType, int amount);
    public int GetTotalPopularity();
    public void SetStrength(GroupType groupType, int strength);
    public void DecreaseStrength(GroupType groupType);
    public bool DoesMainPopulationHatePlayer();
    public void ApplyPopularityChange(string groupPopularityChanges);
    public void ApplyStrengthChange(string groupStrengthChanges);
    public void ResetStatusAndAllies();
}

/// <summary>
///     Provides functionality related to the groups or factions that exist within the game context.
/// </summary>
public class GroupService : IGroupService
{
    private Group[] _groups;
    private const int MaxPopularityAndStrength = 9;
    private const int MinPopularityAndStrength = 0;
    private readonly IRandomService _randomService;

    public GroupService(IRandomService randomService)
    {
        _randomService = randomService;
    }

    public void Initialise()
    {
        _groups = [
            new Group(GroupType.Army, 7, 6, "The ARMY", "   ARMY   "),
            new Group(GroupType.Peasants, 7, 6, "The PEASANTS", " PEASANTS "),
            new Group(GroupType.Landowners, 7, 6, "The LANDOWNERS", "LANDOWNERS"),
            new Group(GroupType.Guerillas, 0, 6, "The GUERILLAS", "GUERILLAS "),
            new Group(GroupType.Leftotans, 7, 6, "The LEFTOTANS", "LEFTOTANS "),
            new Group(GroupType.SecretPolice, 7, 6, "The SECRET POLICE", " S.POLICE "),
            new Group(GroupType.Russians, 7, 0, "The RUSSIANS", " RUSSIANS "),
            new Group(GroupType.Americans, 7, 0, "The AMERICANS", "AMERICANS "),
        ];
    }

    public int GetPopularityByGroupType(GroupType groupType)
    {
        return _groups[(int)groupType].Popularity;
    }

    public int GetStrengthByGroupType(GroupType groupType)
    {
        return _groups[(int)groupType].Strength;
    }

    public GroupType GetGroupTypeByCountry(LenderCountry country)
    {
        GroupType groupType;

        switch (country)
        {
            case LenderCountry.America:
                groupType = GroupType.Americans;
                break;

            case LenderCountry.Russia:
                groupType = GroupType.Russians;
                break;

            default:
                throw new InvalidEnumArgumentException(nameof(country), (int)country, country.GetType());
        }

        return groupType;
    }

    public Group GetGroupByType(GroupType groupType)
    {
        return _groups[(int)groupType];
    }

    public string GetGroupNameByIndex(int index)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _groups.Length);

        return _groups[index].Name;
    }

    public Group[] GetGroups()
    {
        return (Group[])_groups.Clone();
    }

    public Group GetGroupById(int groupId)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(groupId, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(groupId, _groups.Length);

        return _groups[groupId - 1];
    }

    public void SetPopularity(GroupType groupType, int popularity)
    {
        int index = (int)groupType;

        _groups[index].Popularity = Math.Clamp(popularity, MinPopularityAndStrength, MaxPopularityAndStrength);
    }

    public void IncreasePopularity(GroupType groupType, int amount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 1);

        int index = (int)groupType;

        if (_groups[index].Popularity + amount < MaxPopularityAndStrength)
        {
            _groups[index].Popularity += amount;
        }
        else
        {
            _groups[index].Popularity = MaxPopularityAndStrength;
        }
    }

    /// <summary>
    ///     Decreases the popularity of a group by a specified amount.
    /// </summary>
    /// <param name="groupType">The group type which have the decrease in popularity.</param>
    /// <param name="amount">The amount by which the popularity will be decreased.</param>
    public void DecreasePopularity(GroupType groupType, int amount)
    {
        int index = (int)groupType;

        if (_groups[index].Popularity - amount > MinPopularityAndStrength)
        {
            _groups[index].Popularity -= amount;
        }
        else
        {
            _groups[index].Popularity = MinPopularityAndStrength;
        }
    }

    /// <summary>
    ///     Gets the sum of all the current popularities of each individual group.
    /// </summary>
    /// <returns>The total popularity count resulting of the sum of each popularity of the individual groups.</returns>
    public int GetTotalPopularity()
    {
        return _groups.Sum(x => x.Popularity);
    }

    public void SetStrength(GroupType groupType, int strength)
    {
        int index = (int)groupType;

        _groups[index].Strength = Math.Clamp(strength, MinPopularityAndStrength, MaxPopularityAndStrength);
    }

    public void DecreaseStrength(GroupType groupType)
    {
        int index = (int)groupType;

        if (_groups[index].Strength > MinPopularityAndStrength)
        {
            _groups[index].Strength--;
        }
    }

    /// <summary>
    ///     Determines if the main population of Ritimba, which is composed of the army, peasants and landowners, hates the player to the point
    ///     of wanting to carry out an assassination.
    /// </summary>
    /// <returns><c>true</c> if the main population of Ritimba hates the player; otherwise, <c>false</c>.</returns>
    public bool DoesMainPopulationHatePlayer()
    {
        if (_groups[(int)GroupType.Army].Status == GroupStatus.Assassination &&
            _groups[(int)GroupType.Peasants].Status == GroupStatus.Assassination &&
            _groups[(int)GroupType.Landowners].Status == GroupStatus.Assassination)
        {
            return true;
        }

        return false;
    }

    public void ApplyPopularityChange(string groupPopularityChanges)
    {
        for (int i = 0; i < 8; i++)
        {
            if (groupPopularityChanges[i] != 'M')
            {
                int popularity = _groups[i].Popularity + groupPopularityChanges[i] - 'M';

                _groups[i].Popularity = Math.Clamp(popularity, MinPopularityAndStrength, MaxPopularityAndStrength);
            }
        }
    }

    public void ApplyStrengthChange(string groupStrengthChanges)
    {
        for (int i = 0; i < 6; i++)
        {
            if (groupStrengthChanges[i] != 'M')
            {
                int strength = _groups[i].Strength + groupStrengthChanges[i] - 'M';

                _groups[i].Strength = Math.Clamp(strength, MinPopularityAndStrength, MaxPopularityAndStrength);
            }
        }
    }

    /// <summary>
    ///     Resets the status and current ally for the army, peasants and landowners groups.
    /// </summary>
    public void ResetStatusAndAllies()
    {
        for (int i = 0; i < 3; i++)
        {
            _groups[i].Status = GroupStatus.Default;
            _groups[i].Ally = null;
        }
    }
}
