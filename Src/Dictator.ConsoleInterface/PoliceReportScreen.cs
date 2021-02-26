using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PoliceReportScreen : IPoliceReportScreen
    {
        private readonly IGovernmentStats governmentStats;

        public PoliceReportScreen(IGovernmentStats governmentStats)
        {
            this.governmentStats = governmentStats;
        }

        public void Draw()
        {
            Console.Clear();
            ConsoleEx.WriteAt(24, 1, "################################");
            ConsoleEx.WriteAt(24, 3, "      SECRET POLICE REPORT      ");

            ConsoleEx.WriteAt(24, 0, $"MONTH {governmentStats.Month}                       ");
            ConsoleEx.WriteAt(24, 3, "         POLICE  REPORT        ");
            ConsoleEx.WriteAt(24, 6, " POPULARITY          STRENGTHS ");

            // TODO: print group information

            ConsoleEx.WriteAt(24, 17, $"  Your STRENGTH is {governmentStats.PlayerStrength}           ");
            ConsoleEx.WriteAt(24, 19, $"  STRENGTH for REVOLUTION is {governmentStats.RevolutionStrength} ");

            Console.ReadKey();
        }
    }
}
