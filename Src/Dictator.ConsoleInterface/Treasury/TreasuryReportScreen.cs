using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using Dictator.Core.Services;
using System;

namespace Dictator.ConsoleInterface.Treasury
{
    public class TreasuryReportScreen : ITreasuryReportScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        private readonly IAccountService accountService;

        public TreasuryReportScreen(IPressAnyKeyControl pressAnyKeyControl, IAccountService accountService)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.accountService = accountService;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Green);
            ConsoleEx.Clear('$');
            ConsoleEx.WriteAt(2, 4, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(2, 5, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(2, 6, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(8, 9, "TREASURY REPORT", ConsoleColor.White, ConsoleColor.Black);

            string balanceWording = (accountService.GetTreasuryBalance() > 0) ? "holds" : "OWES";

            ConsoleEx.WriteAt(2, 13, $" The TREASURY {balanceWording} ${accountService.GetTreasuryBalance()},000 ", ConsoleColor.Blue, ConsoleColor.White);
            ConsoleEx.WriteAt(3, 15, $" MONTHLY COSTS are ${accountService.GetMonthlyCosts()},000 ", ConsoleColor.Blue, ConsoleColor.White);

            if (accountService.HasSwissBankAccount())
            {
                ConsoleEx.WriteAt(3, 17, $" [SWISS Acct holds ${accountService.GetSwissBankAccountBalance()},000] ");
            }

            pressAnyKeyControl.Show();
        }
    }
}
