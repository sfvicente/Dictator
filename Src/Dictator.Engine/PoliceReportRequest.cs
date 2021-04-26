﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    /// <summary>
    ///     Represents a request for a detailed account or statement from police on the state of the groups and government.
    /// </summary>
    public class PoliceReportRequest
    {
        /// <summary>
        ///     Gets or sets a value that indicates whether the treasury has enough balance to display a report.
        /// </summary>
        public bool HasEnoughBalance { get; set; }

        /// <summary>
        ///     Gets or sets a value that indicates whether the player has enough popularity with the police to display a report.
        /// </summary>
        public bool HasEnoughPopularityWithPolice { get; set; }

        /// <summary>
        ///     Gets or sets a value that indicates whether the police has enough strength to display a report.
        /// </summary>
        public bool HasPoliceEnoughStrength { get; set; }

        /// <summary>
        ///     Gets or sets the current police strength. 
        /// </summary>
        public int PoliceStrength { get; set; }

        /// <summary>
        ///     Gets or sets the current police popularity. 
        /// </summary>
        public int PolicePopularity { get; set; }
    }
}
