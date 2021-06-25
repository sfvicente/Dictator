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
        /// <returns></returns>
        public bool IsPlayerAbleToEscapeByHelicopter();
    }
}
