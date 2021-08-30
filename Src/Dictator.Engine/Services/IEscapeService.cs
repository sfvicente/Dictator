using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    /// <summary>
    ///     Provides functionality related to the player escape from Ritimba.
    /// </summary>
    public interface IEscapeService
    {
        /// <summary>
        ///     Determines if the player is able to escape after the war is lost. There is a 2/3 chances that the player is able to escape.
        /// </summary>
        /// <returns><c>true</c> if manages to escape after losing the war; otherwise, <c>false</c>.</returns>
        public bool IsPlayerAbleToEscapeAfterLosingWar();

        /// <summary>
        ///     Determines if the player is able to escape by helicopter. There is a 1/4 chances that the helicopter won't start.
        /// </summary>
        /// <returns><c>true</c> if the player is able to escape by helicopter; otherwise, <c>false</c>.</returns>
        public bool IsPlayerAbleToEscapeByHelicopter();

        /// <summary>
        ///     Checks if the player is captured by the guerrillas when attempting to escape through leftoto.
        /// </summary>
        /// <returns><c>true</c> if the guerrilas capture the player while attempting escape; otherwise, <c>false</c>.</returns>
        public bool DoesGuerrillaCatchPlayerEscaping();
    }
}
