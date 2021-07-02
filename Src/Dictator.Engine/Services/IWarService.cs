using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IWarService
    {
        /// <summary>
        ///     Determines if a war between the Ritimba republic and Leftoto should happen. There is a 1/3 probability that war should happen.
        /// </summary>
        /// <returns><c>true</c> if war should happen; otherwise, <c>false</c>.</returns>
        public bool ShouldWarHappen();

        /// <summary>
        ///     Applies the effects of a threat of war with leftoto, which results in an increase of the player's popularity amongst the army,
        ///     peasants, landowners and secret police.
        /// </summary>
        public void ApplyThreatOfWarEffects();

        /// <summary>
        ///     Calculates the strength of the Ritimba republic in a scenario of war.
        /// </summary>
        /// <returns>The total strength of Ritimba in a scenario of war.</returns>
        public int CalculateRitimbaStrength();

        /// <summary>
        ///     Calculates the strength of Leftoto in a scenario of war.
        /// </summary>
        /// <returns>The total strength of Leftoto.</returns>
        public int CalculateLeftotoStrength();

        /// <summary>
        ///     Executes and calculates the outcome for the war scenario between the Ritimba and Leftoto.
        /// </summary>
        /// <param name="warStats">The stats required for the war to calculate who wins.</param>
        /// <returns><c>true</c> if Ritimba wins the war; otherwise, <c>false</c>.</returns>
        public bool ExecuteWar(WarStats warStats);

        /// <summary>
        ///     Determines if there is conflict between the republic of Ritimba and Leftoto. A conflict does not exist if the player is popular
        ///     with the leftotans or leftotans are weak.
        /// </summary>
        /// <returns></returns>
        public bool DoesConflictExist();

        /// <summary>
        ///     Initiates an invasion of the Ritimba Republic by Leftoto, which calculates teh strength of both countries in a
        ///     scenario of war.
        /// </summary>
        /// <returns>The war statistics which are composed of the strength of each country.</returns>
        public WarStats BeginInvasion();
    }
}
