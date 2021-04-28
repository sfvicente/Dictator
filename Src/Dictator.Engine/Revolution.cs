using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class Revolution
    {
        /// <summary>
        ///     Gets or sets the current player's strength.
        /// </summary>
        public int PlayerStrength { get; set; }

        /// <summary>
        ///     Gets or sets the current revolution strength. 
        /// </summary>
        public int RevolutionStrength { get; set; }

        /// <summary>
        ///     Gets or sets the current group that has started the revolution.
        /// </summary>
        public Group RevolutionaryGroup { get; set; }

        /// <summary>
        ///     Gets or sets the ally of the current group that has started the revolution.
        /// </summary>
        public Group RevolutionGroupAlly { get; set; }
    }
}