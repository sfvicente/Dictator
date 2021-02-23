using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Engine
{
    public class Game
    {
        private Group[] groups;

        private void InitialiseGroups()
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
    }
}
