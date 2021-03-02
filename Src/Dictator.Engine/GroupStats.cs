using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class GroupStats : IGroupStats
    {
        private Group[] groups;

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
    }
}
