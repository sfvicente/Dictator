using System;

namespace Dictator.Core.Services
{
    /// <summary>
    ///     Provides functionality related to the player escape from Ritimba.
    /// </summary>

    public class EscapeService : IEscapeService
    {
        private readonly IGroupService groupService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EscapeService"/> class from a <see cref="IGroupService"/>
        ///     component.
        /// </summary>
        /// <param name="groupService">The service used to provides functionality related to the groups or factions.</param>
        public EscapeService(IGroupService groupService)
        {
            this.groupService = groupService;
        }

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
        /// <returns><c>true</c> if the player is able to escape by helicopter; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        ///     Checks if the player is captured by the guerrillas when attempting to escape through leftoto.
        /// </summary>
        /// <returns><c>true</c> if the guerrilas capture the player while attempting escape; otherwise, <c>false</c>.</returns>
        public bool DoesGuerrillaCatchPlayerEscaping()
        {
            int guerrilasStrength = groupService.GetGroupByType(GroupType.Guerillas).Strength;
            int upperLimit = (guerrilasStrength / 3) + 2;

            Random random = new Random();
            int number = random.Next(0, upperLimit);

            // There is a chance of 1 in 2..5 that player is captured, which depends on the guerrilas strength
            if (number == 0)
            {
                return true;
            }

            return false;
        }
    }
}
