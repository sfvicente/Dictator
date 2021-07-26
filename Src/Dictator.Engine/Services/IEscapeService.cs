using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IEscapeService
    {
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
