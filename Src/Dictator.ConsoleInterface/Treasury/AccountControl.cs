using Dictator.Core.Models;
using System;
using System.Runtime.CompilerServices;

namespace Dictator.ConsoleInterface.Treasury
{
    /// <summary>
    ///     Represents the control that displays the current balances of both the treasury and Swiss bank account, and
    ///     also displays the monthly costs.
    /// </summary>
    public interface IAccountControl
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="account">The account information regarding the treasury, monthly costs and Swiss account.</param>
        public void Show(Account account);
    }

    /// <summary>
    ///     Represents the control that displays the current balances of both the treasury and Swiss bank account, and
    ///     also displays the monthly costs.
    /// </summary>
    public class AccountControl : BaseScreen, IAccountControl
    {
        public AccountControl(IConsoleService consoleService)
            : base(consoleService)
        {
            
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="account">The account information regarding the treasury, monthly costs and Swiss account.</param>
        public void Show(Account account)
        {
            string balanceWording = (account.TreasuryBalance > 0) ? "holds" : "OWES";

            _consoleService.WriteAt(2, 13, $" The TREASURY {balanceWording} ${account.TreasuryBalance},000 ", ConsoleColor.Blue, ConsoleColor.White);
            _consoleService.WriteAt(3, 15, $" MONTHLY COSTS are ${account.MonthlyCosts},000 ", ConsoleColor.Blue, ConsoleColor.White);

            if (account.HasSwissBankAccount)
            {
                _consoleService.WriteAt(3, 18, $"[SWISS Acct holds ${account.SwissBankAccountBalance},000]", ConsoleColor.Blue, ConsoleColor.White);
            }
        }
    }
}
