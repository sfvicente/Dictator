using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IGroupStats
    {
        public GroupType AssassinGroupType { get; }

        public void Initialise();
        public Group[] GetGroups();
        public int GetPopularityByGroupType(GroupType groupType);
        public int GetStrengthByGroupType(GroupType groupType);
        public Group GetGroupByType(GroupType groupType);
        public string GetGroupNameByIndex(int index);
        public void SetPopularity(GroupType groupType, int popularity);
        public void IncreasePopularity(GroupType groupType);
        public void DecreasePopularity(GroupType groupType);
        public void DecreasePopularity(GroupType groupType, int amount);
        public int GetTotalPopularity();
        public void SetStrength(GroupType groupType, int strength);
        public void DecreaseStrength(GroupType groupType);
        public void SetAssassinByGroupType(GroupType groupType);
        public bool DoesMainPopulationHatePlayer();
        public void ApplyPopularityChange(string groupPopularityChanges);
        public void ApplyStrengthChange(string groupStrengthChanges);
        public void ResetStatusAndAllies();
    }
}
