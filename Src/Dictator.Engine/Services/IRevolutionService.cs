using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IRevolutionService
    {
        /// <summary>
        ///     Attempts to assign a revolt group in a scenario of revolution.
        /// </summary>
        /// <returns><c>true</c> if one of the groups becomes a group responsible for initiating a revolution; otherwise, <c>false</c>.</returns>
        public bool TryTriggerRevoltGroup();

        /// <summary>
        ///     Finds the groups that can be possible allies of a player in a revolution.
        /// </summary>
        /// <returns>A dictionary containing the groups that can be possible allies with their respective ids.</returns>
        public Dictionary<int, Group> FindPossibleAllies();
        
        /// <summary>
        ///     Determines if a group accepts to be an ally of the player during a revolution.
        /// </summary>
        /// <param name="groupId">The id of the group to accept or refuse an alliance.</param>
        /// <returns><c>true</c> if the group accepts to be an ally; otherwise, <c>false</c>.</returns>
        public bool DoesGroupAcceptAllianceInRevolution(int groupId);

        public void SetPlayerAllyForRevolution(int selectedAllyGroupId);
        public void SetRevolutionaryGroup(Group revolutionaryGroup);

        /// <summary>
        ///     Determines if a revolution has succeeded in overthrowing the player.
        /// </summary>
        /// <returns><c>true</c> if the revolution has succeeded; otherwise, <c>false</c>.</returns>
        public bool DoesRevolutionSucceed();

        /// <summary>
        ///     Punishes the groups that have taken part in a revolution that has been offset by the player. It
        ///     sets the revolutionaries strength and popularity to zero.
        /// </summary>
        public void PunishRevolutionaries();

        public void ApplyRevolutionCrushedEffects();
    }
}