using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dictator.Core.Services;

/// <summary>
///     Provides functionality related to the groups or factions that exist within the game context.
/// </summary>
public interface IGroupService
{
    public GroupType AssassinGroupType { get; }

    public void Initialise();
    public Group[] GetGroups();

    /// <summary>
    ///     Retrieves a specific group by searching for the group id.
    /// </summary>
    /// <param name="groupId">The id of the group to search for.</param>
    /// <returns>The group whose id matches the criteria.</returns>
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

    /// <summary>
    ///     Sets the level of strength for a specific group.
    /// </summary>
    /// <param name="groupType">The type of the group to set the strength.</param>
    /// <param name="strength">The strength level which will be set.</param>
    public void SetStrength(GroupType groupType, int strength);

    public void DecreaseStrength(GroupType groupType);
    public void SetAssassinByGroupType(GroupType groupType);
    public bool DoesMainPopulationHatePlayer();
    public void ApplyPopularityChange(string groupPopularityChanges);
    public void ApplyStrengthChange(string groupStrengthChanges);

    /// <summary>
    ///     Determines if an assassination attempt on the player should happen by one of the following groups: army, 
    ///     peasants, landowners and guerrilas.
    /// </summary>
    /// <returns><c>true</c> if an assassination attempt should happen; otherwise, <c>false</c>.</returns>
    public bool ShouldAssassinationAttemptHappen();

    public void ResetStatusAndAllies();
}

/// <summary>
///     Provides functionality related to the groups or factions that exist within the game context.
/// </summary>
public class GroupService : IGroupService
{
    private Group[] groups;
    private const int MaxPopularityAndStrength = 9;
    private const int MinPopularityAndStrength = 0;
    private readonly IRandomService _randomService;
    private GroupType assassinGroupType;
    public GroupType AssassinGroupType { get { return assassinGroupType; } }

    public GroupService(IRandomService randomService)
    {
        _randomService = randomService;
    }

    public void Initialise()
    {
        groups = new Group[]
        {
            new Group(GroupType.Army, 7, 6, "The ARMY", "   ARMY   "),
            new Group(GroupType.Peasants, 7, 6, "The PEASANTS", " PEASANTS "),
            new Group(GroupType.Landowners, 7, 6, "The LANDOWNERS", "LANDOWNERS"),
            new Group(GroupType.Guerillas, 0, 6, "The GUERILLAS", "GUERILLAS "),
            new Group(GroupType.Leftotans, 7, 6, "The LEFTOTANS", "LEFTOTANS "),
            new Group(GroupType.SecretPolice, 7, 6, "The SECRET POLICE", " S.POLICE "),
            new Group(GroupType.Russians, 7, 0, "The RUSSIANS", " RUSSIANS "),
            new Group(GroupType.Americans, 7, 0, "The AMERICANS", "AMERICANS "),
        };
    }

    public int GetPopularityByGroupType(GroupType groupType)
    {
        return groups[(int)groupType].Popularity;
    }

    public int GetStrengthByGroupType(GroupType groupType)
    {
        return groups[(int)groupType].Strength;
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
        return groups[(int)groupType];
    }

    public string GetGroupNameByIndex(int index)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, groups.Length);

        return groups[index].Name;
    }

    public Group[] GetGroups()
    {
        return (Group[])groups.Clone();
    }

    /// <summary>
    ///     Retrieves a specific group by searching for the group id.
    /// </summary>
    /// <param name="groupId">The id of the group to search for.</param>
    /// <returns>The group whose id matches the criteria.</returns>
    public Group GetGroupById(int groupId)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(groupId, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(groupId, groups.Length);

        return groups[groupId - 1];
    }

    public void SetPopularity(GroupType groupType, int popularity)
    {
        int index = (int)groupType;

        groups[index].Popularity = GetBoundedAttribute(popularity);
    }

    /// <summary>
    ///     Increases the popularity of a group by a specified amount.
    /// </summary>
    /// <param name="groupType">The group type which have the increase in popularity.</param>
    /// <param name="amount">The amount by which the popularity will be increased.</param>
    public void IncreasePopularity(GroupType groupType, int amount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 1);

        int index = (int)groupType;

        if (groups[index].Popularity + amount < MaxPopularityAndStrength)
        {
            groups[index].Popularity += amount;
        }
        else
        {
            groups[index].Popularity = MaxPopularityAndStrength;
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

        if (groups[index].Popularity - amount > MinPopularityAndStrength)
        {
            groups[index].Popularity -= amount;
        }
        else
        {
            groups[index].Popularity = MinPopularityAndStrength;
        }
    }

    /// <summary>
    ///     Gets the sum of all the current popularities of each individual group.
    /// </summary>
    /// <returns>The total popularity count resulting of the sum of each popularity of the individual groups.</returns>
    public int GetTotalPopularity()
    {
        return groups.Sum(x => x.Popularity);
    }

    /// <summary>
    ///     Sets the level of strength for a specific group.
    /// </summary>
    /// <param name="groupType">The type of the group to set the strength.</param>
    /// <param name="strength">The strength level which will be set.</param>
    public void SetStrength(GroupType groupType, int strength)
    {
        int index = (int)groupType;

        groups[index].Strength = GetBoundedAttribute(strength);
    }

    public void DecreaseStrength(GroupType groupType)
    {
        int index = (int)groupType;

        if (groups[index].Strength > MinPopularityAndStrength)
        {
            groups[index].Strength--;
        }
    }

    public void SetAssassinByGroupType(GroupType groupType)
    {
        assassinGroupType = groupType;
    }

    /// <summary>
    ///     Determines if the main population of Ritimba, which is composed of the army, peasants and landowners, hates the player to the point
    ///     of wanting to carry out an assassination.
    /// </summary>
    /// <returns><c>true</c> if the main population of Ritimba hates the player; otherwise, <c>false</c>.</returns>
    public bool DoesMainPopulationHatePlayer()
    {
        if (groups[(int)GroupType.Army].Status == GroupStatus.Assassination &&
            groups[(int)GroupType.Peasants].Status == GroupStatus.Assassination &&
            groups[(int)GroupType.Landowners].Status == GroupStatus.Assassination)
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
                int popularity = groups[i].Popularity + groupPopularityChanges[i] - 'M';

                groups[i].Popularity = GetBoundedAttribute(popularity);
            }
        }
    }

    /// <summary>
    ///     Applies the specified strength changes to all groups except Americans and Russians.
    /// </summary>
    /// <param name="groupStrengthChanges"></param>
    public void ApplyStrengthChange(string groupStrengthChanges)
    {
        for (int i = 0; i < 6; i++)
        {
            if (groupStrengthChanges[i] != 'M')
            {
                int strength = groups[i].Strength + groupStrengthChanges[i] - 'M';

                groups[i].Strength = GetBoundedAttribute(strength);
            }
        }
    }

    private int GetBoundedAttribute(int attributeValue)
    {
        if (attributeValue < MinPopularityAndStrength)
        {
            return MinPopularityAndStrength;
        }

        if (attributeValue > MaxPopularityAndStrength)
        {
            return MaxPopularityAndStrength;
        }

        return attributeValue;
    }

    /// <summary>
    ///     Determines if an assassination attempt on the player should happen by one of the following groups: army, 
    ///     peasants, landowners and guerrilas.
    /// </summary>
    /// <returns><c>true</c> if an assassination attempt should happen; otherwise, <c>false</c>.</returns>
    public bool ShouldAssassinationAttemptHappen()
    {
        int number = _randomService.Next(3);

        // Select a random group between the army, peasants, landowners and guerrilas
        if (groups[number].Status == GroupStatus.Assassination)
        {
            SetAssassinByGroupType(groups[number].Type);
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Resets the status and current ally for the army, peasants and landowners groups.
    /// </summary>
    public void ResetStatusAndAllies()
    {
        for (int i = 0; i < 3; i++)
        {
            groups[i].Status = GroupStatus.Default;
            groups[i].Ally = null;
        }
    }
}
