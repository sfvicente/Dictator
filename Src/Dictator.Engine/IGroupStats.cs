using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IGroupStats
    {
        public int PolicePopularity { get; }
        public int PoliceStrength { get; }
        public int TotalPopularity { get; }
        public GroupType AssassinGroupType { get; }

        void Initialise();
        public Group[] GetGroups();
        Group GetGroupByType(GroupType groupType);
        string GetGroupNameByIndex(int index);
        public void DecreasePopularity(GroupType groupType);
        public void DecreaseStrength(GroupType groupType);
        void SetAssassinByGroupType(GroupType groupType);
        bool DoesMainPopulationHatePlayer();
        public void ApplyPopularityChange(string groupPopularityChanges);
        public void ApplyStrengthChange(string groupStrengthChanges);
    }
}
