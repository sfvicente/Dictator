using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    /// <summary>
    ///     Represents the attributes of a scenario of war between the Ritimba republic and Leftoto.
    /// </summary>
    public class WarStats
    {
        /// <summary>
        ///     Gets or sets the war strength of Ritimba. 
        /// </summary>
        public int RitimbanStrength { get; set; }

        /// <summary>
        ///     Gets or sets the war strength of Leftoto. 
        /// </summary>
        public int LeftotanStrength { get; set; }
    }
}
