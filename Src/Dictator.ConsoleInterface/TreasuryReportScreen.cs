﻿using Dictator.Common.Extensions;
using Dictator.Core;
using System;

namespace Dictator.ConsoleInterface
{
    public class TreasuryReportScreen : ITreasuryReportScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        private readonly IAccount account;

        public TreasuryReportScreen(IPressAnyKeyControl pressAnyKeyControl, IAccount account)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.account = account;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Green);
            ConsoleEx.Clear('$');
            ConsoleEx.WriteAt(2, 4, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(2, 5, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(2, 6, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(8, 9, "TREASURY REPORT", ConsoleColor.White, ConsoleColor.Black);

            string balanceWording = (account.TreasuryBalance > 0) ? "holds" : "OWES";

            ConsoleEx.WriteAt(2, 13, $" The TREASURY {balanceWording} ${account.TreasuryBalance},000 ", ConsoleColor.Blue, ConsoleColor.White);
            ConsoleEx.WriteAt(3, 15, $" MONTHLY COSTS are ${account.MonthlyCosts},000 ", ConsoleColor.Blue, ConsoleColor.White);

            if (account.HasSwissBankAccount)
            {
                ConsoleEx.WriteAt(3, 17, $" [SWISS Acct holds ${account.SwissBankAccountBalance},000] ");
            }

            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
