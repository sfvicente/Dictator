using Dictator.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.Treasury
{
    /// <summary>
    ///     Represents the control that displays the current balances of both the treasury and Swiss bank account, and
    ///     also displays the monthly costs.
    /// </summary>
    public class AccountControl : IAccountControl
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="account">The account information regarding the treasury, monthly costs and Swiss account.</param>
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
