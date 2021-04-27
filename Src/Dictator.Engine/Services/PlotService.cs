using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
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
            if (governmentStats.Month < 2)
            {
                return;
            }

            groupStats.ResetStatusAndAllies();

            // TODO: plot bonus check

            Group[] groups = groupStats.GetGroups();
            int monthlyMinimalPopularityAndStrength = governmentStats.MonthlyMinimalPopularityAndStrength;
            int monthlyRevolutionStrength = governmentStats.MonthlyRevolutionStrength;

            // Perform internal plot logic
            ExecutePlot(groups, monthlyMinimalPopularityAndStrength, monthlyRevolutionStrength);
        }

        private void ExecutePlot(Group[] groups, int monthlyMinimalPopularityAndStrength, int monthlyRevolutionStrength)
        {
            for (int i = 0; i < 3; i++)
            {
                // Only groups with popularity less or equal to the monthly minimal popularity plot against the player
                if (groups[i].Popularity <= monthlyMinimalPopularityAndStrength)
                {
                    // Cycle through the groups to find allies for the revolution
                    for (int k = 0; k < 6; k++)
                    {
                        // Skip this pairing if we are dealing with the same group or a group that has popularity above the minimal monthly popularity required
                        if (i == k || groups[k].Popularity > monthlyMinimalPopularityAndStrength)
                            continue;

                        // Verify if the strength of both groups combined is equal or greater than the monthly defined minimal revolution strength
                        if (groups[i].Strength + groups[k].Strength >= monthlyRevolutionStrength)
                        {
                            // If so, set the ally for the original group and the appropriate status
                            groups[i].Status = GroupStatus.Revolution;
                            groups[i].Ally = groups[k];
                            break;
                        }
                    }

                    // If no allies could be found for the group, set the group status to assassination
                    if (groups[i].Status == GroupStatus.Default)
                    {
                        groups[i].Status = GroupStatus.Assassination;
                    }
                }
            }

        }
    }
}
