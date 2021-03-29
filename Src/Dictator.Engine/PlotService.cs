﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class PlotService : IPlotService
    {
        private readonly IGroupStats groupStats;
        private readonly IGovernmentStats governmentStats;

        public PlotService(IGroupStats groupStats, IGovernmentStats governmentStats)
        {
            this.groupStats = groupStats;
            this.governmentStats = governmentStats;
        }

        public void Plot()
        {
            // Do not trigger assassinations or revolutions in the first 2 months of government
            if(governmentStats.Month < 2)
            {
                return;
            }

            ResetStates();

            // TODO: plot bonus check

            // TODO: perform plot logic
        }

        public void ResetStates()
        {
            // TODO: Implement
        }
    }
}
