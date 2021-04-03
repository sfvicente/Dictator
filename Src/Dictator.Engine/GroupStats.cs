using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictator.Core
{
    public class GroupStats : IGroupStats
    {
        private Group[] groups;

        public int TotalPopularity { get => groups.Sum(x => x.Popularity); }

        private GroupType assassinGroupType;
        public GroupType AssassinGroupType { get { return assassinGroupType; } }

        public int PolicePopularity { get { return groups[(int)GroupType.SecretPolice].Popularity; } }
        public int PoliceStrength { get { return groups[(int)GroupType.SecretPolice].Strength; } }

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

        public void IncreasePopularity(GroupType groupType)
        {
            int index = (int)groupType;

            groups[index].Popularity++;
        }

        public void DecreasePopularity(GroupType groupType)
        {
            int index = (int)groupType;

            if (groups[index].Popularity > 0)
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

        public void IncreaseStrength(GroupType groupType)
        {
            int index = (int)groupType;

            groups[index].Strength++;
        }

        public void DecreaseStrength(GroupType groupType)
        {
            int index = (int)groupType;

            if (groups[index].Strength > 0)
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
        /// <returns></returns>
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

        public void ApplyStrengthChange(string groupStrengthChanges)
        {
            // Strength changes are applied to all groups except Americans and Russians
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
            if(attributeValue < 0)
            {
                return 0;
            }

            if(attributeValue > 9)
            {
                return 9;
            }

            return attributeValue;
        }

        /// <summary>
        ///     Resets the status and current ally for the army, peasants and landowners groups.
        /// </summary>
        public void ResetStatusAndAllies()
        {
            for(int i = 0; i < 3; i++)
            {
                groups[i].Status = GroupStatus.Default;
                groups[i].Ally = null;
            }
        }
    }
}
