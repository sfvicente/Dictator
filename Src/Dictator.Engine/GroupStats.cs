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
    }
}
