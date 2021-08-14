using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Treasury;
using Dictator.Core;
using System;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public class TransferToSwissBankAccountScreen : ITransferToSwissBankAccountScreen
    {
        private readonly IAccountControl accountControl;
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public TransferToSwissBankAccountScreen(IAccountControl accountControl, IPressAnyKeyControl pressAnyKeyControl)
        {
            this.accountControl = accountControl;
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

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
