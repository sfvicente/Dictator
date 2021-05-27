using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IRevolutionService
    {
        public Dictionary<int, Group> FindPossibleAllies();
        
        /// <summary>
        ///     Determines if a group accepts to be an ally of the player during a revolution.
        /// </summary>
        /// <param name="groupId">The id of the group to accept or refuse an alliance.</param>
        /// <returns><c>true</c> if the group accepts to be an ally; otherwise, <c>false</c>.</returns>
        public bool DoesGroupAcceptAllianceInRevolution(int groupId);

        public void SetPlayerAllyForRevolution(int selectedAllyGroupId);
        public void SetRevolutionaryGroup(Group revolutionaryGroup);
        public void PunishRevolutionaries();
        public void BoostAllyPopularity();
    }
}