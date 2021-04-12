using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public class TransferToSwissBankAccount : ITransferToSwissBankAccount
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public TransferToSwissBankAccount(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show(SwissBankAccountTransfer swissBankAccountTransfer)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 3, "TRANSFER to a SWISS BANK ACCOUNT");

            if (swissBankAccountTransfer.AmountStolen > 0)
            {
                ConsoleEx.WriteAt(1, 11, $"The TREASURY held ${swissBankAccountTransfer.TreasuryBalance}, 000");
                ConsoleEx.WriteAt(1, 13, $"${swissBankAccountTransfer.AmountStolen}, 000 has been TRANSFERRED");
            }
            else
            {
                ConsoleEx.WriteAt(8, 11, "NO TRANSFER made");
            }

            pressAnyKeyControl.Show();
        }
    }
}
