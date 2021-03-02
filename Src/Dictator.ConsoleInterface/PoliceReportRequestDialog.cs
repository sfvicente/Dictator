using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PoliceReportRequestDialog : IPoliceReportRequestDialog
    {
        private readonly IAccount account;
        private readonly IGroupStats groupStats;
        private readonly IGovernmentStats governmentStats;

        public PoliceReportRequestDialog(IAccount account, IGroupStats groupStats, IGovernmentStats governmentStats)
        {
            this.account = account;
            this.groupStats = groupStats;
            this.governmentStats = governmentStats;
        }

        public bool Show()
        {
            Console.Clear();
            ConsoleEx.WriteAt(24, 1, "################################");
            ConsoleEx.WriteAt(24, 3, "     SECRET POLICE REPORT ?     ");

            if (this.account.TreasuryBalance > 0)
            {
                ConsoleEx.WriteAt(24, 12, "         ( costs $1000 )        ");

                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    this.account.TreasuryBalance -= 1;
                    return true;
                }
            }
            else
            {
                ConsoleEx.WriteAt(24, 10, "          NOT AVAILABLE         ");

                int screenRow = 12;

                if (groupStats.PolicePopularity <= governmentStats.MonthlyMinimalPopularityAndStrength)
                {
                    ConsoleEx.WriteAt(24, screenRow++, $"  Your POPULARITY with us is {groupStats.PoliceStrength}  ");
                }

                if (groupStats.PoliceStrength <= governmentStats.MonthlyMinimalPopularityAndStrength)
                {
                    ConsoleEx.WriteAt(24, screenRow++, $"      POLICE strength is {groupStats.PoliceStrength}      ");
                }

                if (account.TreasuryBalance < 1)
                {
                    ConsoleEx.WriteAt(24, screenRow++, "    You can't AFFORD a REPORT    ");
                }
                Console.ReadKey();
            }

            return false;
        }
    }
}
