using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Treasury;
using Dictator.Core;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    /// <summary>
    ///     Represents the screen that is displayed when the player transfers funds from the treasury 
    ///     to the Swiss bank account.
    /// </summary>
    public class TransferToSwissBankAccountScreen : ITransferToSwissBankAccountScreen
    {
        private readonly IAccountControl accountControl;
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public TransferToSwissBankAccountScreen(IAccountControl accountControl, IPressAnyKeyControl pressAnyKeyControl)
        {
            this.accountControl = accountControl;
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="swissBankAccountTransfer">The details of the Swiss account transfer.</param>
        /// <param name="account">The current treasury and costs information.</param>
        public void Show(SwissBankAccountTransfer swissBankAccountTransfer, Account account)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 4, "TRANSFER to a SWISS BANK ACCOUNT", ConsoleColor.Black, ConsoleColor.DarkYellow);

            if (swissBankAccountTransfer.AmountStolen > 0)
            {
                ConsoleEx.WriteAt(1, 7, $"The TREASURY held ${swissBankAccountTransfer.TreasuryPreviousBalance},000");
                ConsoleEx.WriteAt(1, 10, $"${swissBankAccountTransfer.AmountStolen},000 has been TRANSFERRED");
            }
            else
            {
                ConsoleEx.WriteAt(8, 11, "NO TRANSFER made"); // TODO: fix placement 
            }

            accountControl.Show(account);
            pressAnyKeyControl.Show();
        }
    }
}
