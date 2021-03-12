using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PoliceReportRequestDialog : IPoliceReportRequestDialog
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;
        private readonly IAccount account;
        private readonly IGroupStats groupStats;
        private readonly IGovernmentStats governmentStats;

        public PoliceReportRequestDialog(
            IPressAnyKeyControl pressAnyKeyControl,
            IPressAnyKeyWithYesControl pressAnyKeyWithYesControl,
            IAccount account,
            IGroupStats groupStats,
            IGovernmentStats governmentStats)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
            this.account = account;
            this.groupStats = groupStats;
            this.governmentStats = governmentStats;
        }

        public bool Show()
        {
            Console.Clear();
            ConsoleEx.WriteAt(0, 1, "################################");
            ConsoleEx.WriteAt(0, 3, "     SECRET POLICE REPORT ?     ");

            if (account.TreasuryBalance > 0 && HasEnoughPopularityWithPolice() && HasEnoughPoliceStrength())
            {
                ConsoleEx.WriteAt(0, 12, "         ( costs $1000 )        ");
                pressAnyKeyWithYesControl.Show();

                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    account.TreasuryBalance -= 1;
                    return true;
                }
            }
            else
            {
                ConsoleEx.WriteAt(0, 10, "          NOT AVAILABLE         ");

                int screenRow = 12;

                if (!HasEnoughPopularityWithPolice())
                {
                    ConsoleEx.WriteAt(0, screenRow++, $"  Your POPULARITY with us is {groupStats.PoliceStrength}  ");
                }

                if (!HasEnoughPoliceStrength())
                {
                    ConsoleEx.WriteAt(0, screenRow++, $"      POLICE strength is {groupStats.PoliceStrength}      ");
                }

                if (account.TreasuryBalance < 1)
                {
                    ConsoleEx.WriteAt(0, screenRow++, "    You can't AFFORD a REPORT    ");
                }

                pressAnyKeyControl.Show();
                Console.ReadKey(true);
            }

            return false;
        }

        private bool HasEnoughPopularityWithPolice()
        {
            return groupStats.PolicePopularity > governmentStats.MonthlyMinimalPopularityAndStrength;
        }

        private bool HasEnoughPoliceStrength()
        {
            return groupStats.PoliceStrength > governmentStats.MonthlyMinimalPopularityAndStrength;
        }
    }
}
