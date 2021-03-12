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
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Green);
            
            for(int row = 1; row < 22; row++)
            {
                ConsoleEx.WriteAt(0, row, "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            }
            ConsoleEx.WriteAt(1, 4, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 5, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 6, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(8, 9, "TREASURY REPORT", ConsoleColor.White, ConsoleColor.Black);

            string balanceWording = (this.account.TreasuryBalance > 0) ? "holds" : "OWES";
            
            ConsoleEx.WriteAt(1, 13, $" The TREASURY {balanceWording} ${this.account.TreasuryBalance},000 ", ConsoleColor.Blue, ConsoleColor.White);
            ConsoleEx.WriteAt(2, 15, $" MONTHLY COSTS are ${this.account.MonthlyCosts},000 ", ConsoleColor.Blue, ConsoleColor.White);

            if (this.account.HasSwissBankAccount)
            {
                ConsoleEx.WriteAt(2, 17, $" [SWISS Acct holds ${this.account.SwissBankAccountBalance},000] ");
            }

            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
