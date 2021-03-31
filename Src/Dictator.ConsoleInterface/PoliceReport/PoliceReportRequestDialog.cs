using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PoliceReport
{
    public class PoliceReportRequestDialog : IPoliceReportRequestDialog
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;
        private readonly IAccountService account;
        private readonly IGroupStats groupStats;
        private readonly IGovernmentStats governmentStats;

        public PoliceReportRequestDialog(
            IPressAnyKeyControl pressAnyKeyControl,
            IPressAnyKeyWithYesControl pressAnyKeyWithYesControl,
            IAccountService account,
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
            ConsoleEx.Clear(ConsoleColor.Black, ConsoleColor.White);
            ConsoleEx.WriteAt(1, 1, "################################");
            ConsoleEx.WriteAt(1, 3, "     SECRET POLICE REPORT ?     ");

            if (account.GetTreasuryBalance() > 0 && HasEnoughPopularityWithPolice() && HasEnoughPoliceStrength())
            {
                ConsoleEx.WriteAt(1, 12, "         ( costs $1000 )        ");
                
                DialogResult dialogResult = pressAnyKeyWithYesControl.Show();

                if (dialogResult == DialogResult.Yes)
                {
                    account.ChangeTreasuryBalance(-1);
                    return true;
                }
            }
            else
            {
                ConsoleEx.WriteAt(1, 10, "          NOT AVAILABLE         ");

                int screenRow = 12;

                if (!HasEnoughPopularityWithPolice())
                {
                    ConsoleEx.WriteAt(1, screenRow++, $"  Your POPULARITY with us is {groupStats.PoliceStrength}  ");
                }

                if (!HasEnoughPoliceStrength())
                {
                    ConsoleEx.WriteAt(1, screenRow++, $"      POLICE strength is {groupStats.PoliceStrength}      ");
                }

                if (account.GetTreasuryBalance() < 1)
                {
                    ConsoleEx.WriteAt(1, screenRow++, "    You can't AFFORD a REPORT    ");
                }

                pressAnyKeyControl.Show();
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
