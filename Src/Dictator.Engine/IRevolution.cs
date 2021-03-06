﻿namespace Dictator.Core
{
    public interface IRevolution
    {
        /// <summary>
        ///     Gets or sets the current player's strength.
        /// </summary>
        public int PlayerStrength { get; set; }

        /// <summary>
        ///     Gets or sets the current player's ally.
        /// </summary>
        public Group PlayerAlly { get; set; }

        /// <summary>
        ///     Gets or sets the current revolution strength. 
        /// </summary>
        public int RevolutionStrength { get; set; }

        /// <summary>
        ///     Gets or sets the current group that has started the revolution.
        /// </summary>
        public Group RevolutionaryGroup { get; set; }
    }
}