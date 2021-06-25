using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class EscapeService : IEscapeService
    {
        /// <summary>
        ///     Determines if the player is able to escape after the war is lost. There is a 2/3 chances that the player is able to escape.
        /// </summary>
        /// <returns><c>true</c> if manages to escape after losing the war; otherwise, <c>false</c>.</returns>
        public bool IsPlayerAbleToEscapeAfterLosingWar()
        {
            Random random = new Random();
            int number = random.Next(0, 3);

            // The player has a 2 in 3 chances that he manages to escape
            if (number < 2)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Determines if the player is able to escape by helicopter. There is a 1/4 chances that the helicopter won't start.
        /// </summary>
        /// <returns></returns>
        public bool IsPlayerAbleToEscapeByHelicopter()
        {
            Random random = new Random();
            int number = random.Next(0, 4);

            // The player has a 1 in 4 chances that the helicopter won't start
            if (number != 0)
            {
                // The escape by helicopter is successfull
                return true;
            }

            // The escape by helicopoter fails as the helicopter won't start
            return false;
        }
    }
}
