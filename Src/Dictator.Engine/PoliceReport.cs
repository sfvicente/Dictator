﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Dictator.Core
{
    /// <summary>
    ///     Represents a detailed account or statement from police on the state of the groups and government.
    /// </summary>
    public class PoliceReport
    {
        /// <summary>
        ///     Gets or sets the current month of government. 
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        ///     Gets or sets the detailed group information. 
        /// </summary>
        public ReadOnlyCollection<Group> Groups { get; set; }

        /// <summary>
        ///     Gets or sets the current player's strength.
        /// </summary>
        public int PlayerStrength { get; set; }

        /// <summary>
        ///     Gets or sets the monthly generated revolution strength. 
        /// </summary>
        public int MonthlyRevolutionStrength { get; set; }
    }
}
