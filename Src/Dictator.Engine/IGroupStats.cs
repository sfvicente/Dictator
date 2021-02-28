using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IGroupStats
    {
        void Initialise();
        public Group[] GetGroups();
        public void DecreasePopularity(GroupType groupType);
        public void DecreaseStrength(GroupType groupType);
    }
}
