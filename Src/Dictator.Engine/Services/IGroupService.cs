using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IGroupService
    {
        public GroupType AssassinGroupType { get; }

        public void Initialise();
        public Group[] GetGroups();
        public int GetPopularityByGroupType(GroupType groupType);
        public int GetStrengthByGroupType(GroupType groupType);
        public Group GetGroupByType(GroupType groupType);
        public string GetGroupNameByIndex(int index);
        public void SetPopularity(GroupType groupType, int popularity);
        public void IncreasePopularity(GroupType groupType, int amount);
        public void DecreasePopularity(GroupType groupType);
        public void DecreasePopularity(GroupType groupType, int amount);
        public int GetTotalPopularity();
        public void SetStrength(GroupType groupType, int strength);
        public void DecreaseStrength(GroupType groupType);
        public void SetAssassinByGroupType(GroupType groupType);
        public bool DoesMainPopulationHatePlayer();
        public void ApplyPopularityChange(string groupPopularityChanges);
        public void ApplyStrengthChange(string groupStrengthChanges);

        /// <summary>
        ///     Determines if an assassination attempt should be performed by one of the following groups: army, 
        ///     peasants, landowners and guerrilas.
        /// </summary>
        /// <returns><c>true</c> if an assassination attempt should happen; otherwise, <c>false</c>.</returns>
        public bool ShouldAssassinationAttemptHappen();

        public void ResetStatusAndAllies();
    }
}
