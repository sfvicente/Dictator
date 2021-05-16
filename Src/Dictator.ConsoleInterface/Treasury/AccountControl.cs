using Dictator.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Treasury
{
    public class AccountControl : IAccountControl
    {
        public void Show(Account account)
        {
            string balanceWording = (account.TreasuryBalance > 0) ? "holds" : "OWES";

            ConsoleEx.WriteAt(2, 13, $" The TREASURY {balanceWording} ${account.TreasuryBalance},000 ", ConsoleColor.Blue, ConsoleColor.White);
            ConsoleEx.WriteAt(3, 15, $" MONTHLY COSTS are ${account.MonthlyCosts},000 ", ConsoleColor.Blue, ConsoleColor.White);

            if (account.HasSwissBankAccount)
            {
                ConsoleEx.WriteAt(3, 18, $"[SWISS Acct holds ${account.SwissBankAccountBalance},000]", ConsoleColor.Blue, ConsoleColor.White);
            }
        }
    }
}
