using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictator.Core.Services
{
    public class GroupService : IGroupService
    {
        private Group[] groups;

        public const int MaxPopularityAndStrength = 9;
        public const int MinPopularityAndStrength = 0;

        private GroupType assassinGroupType;
        public GroupType AssassinGroupType { get { return assassinGroupType; } }

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

        public Group GetGroupByType(GroupType groupType)
        {
            return groups[(int)groupType];
        }

        public string GetGroupNameByIndex(int index)
        {
            if (index < 0 || index > groups.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            return groups[index].Name;
        }

        public Group[] GetGroups()
        {
            return (Group[])groups.Clone();
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
            if(amount < 1)
            {
                throw new ArgumentException($"'{nameof(amount)}' must be greater than zero.");
            }

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

        public void DecreasePopularity(GroupType groupType)
        {
            int index = (int)groupType;

            if (groups[index].Popularity > MinPopularityAndStrength)
            {
                groups[index].Popularity--;
            }
        }

        public void DecreasePopularity(GroupType groupType, int amount)
        {
            int index = (int)groupType;

            if (groups[index].Popularity > 0)
            {
                groups[index].Popularity -= amount;
            }
        }

        public int GetTotalPopularity()
        {
            return groups.Sum(x => x.Popularity);
        }

        /// <summary>
        ///     Sets the value of the strength of a specific group.
        /// </summary>
        /// <param name="groupType"></param>
        /// <param name="strength"></param>
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
        ///     Determines if an assassination attempt should be performed by one of the following groups: army, 
        ///     peasants, landowners and guerrilas.
        /// </summary>
        /// <returns><c>true</c> if an assassination attempt should happen; otherwise, <c>false</c>.</returns>
        public bool ShouldAssassinationAttemptHappen()
        {
            Random random = new Random();
            int number = random.Next(0, 3);

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
}
