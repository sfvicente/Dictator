using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class TreasuryReportScreen : ITreasuryReportScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        public IAccount account;

        public TreasuryReportScreen(IPressAnyKeyControl pressAnyKeyControl, IAccount account)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.account = account;
        }

        public void Show()
        {
            Console.Clear();
            ConsoleEx.WriteAt(24, 1, "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            ConsoleEx.WriteAt(24, 3, "         TREASURY REPORT        ");

            string balanceWording = (this.account.TreasuryBalance > 0) ? "holds" : "OWES";
            
            ConsoleEx.WriteAt(24, 10, $"  The TREASURY {balanceWording} ${this.account.TreasuryBalance},000");
            ConsoleEx.WriteAt(24, 12, $"   MONTHLY COSTS are ${this.account.MonthlyCosts},000 ");

            if (this.account.HasSwissBankAccount)
            {
                ConsoleEx.WriteAt(24, 14, $"  [SWISS Acct holds ${this.account.SwissBankAccountBalance},000]");
            }

            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
