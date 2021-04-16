using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public class TransferToSwissBankAccountScreen : ITransferToSwissBankAccountScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public TransferToSwissBankAccountScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show(SwissBankAccountTransfer swissBankAccountTransfer)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 4, "TRANSFER to a SWISS BANK ACCOUNT", ConsoleColor.Black, ConsoleColor.DarkYellow);

            if (swissBankAccountTransfer.AmountStolen > 0)
            {
                ConsoleEx.WriteAt(1, 7, $"The TREASURY held ${swissBankAccountTransfer.TreasuryBalance}, 000");
                ConsoleEx.WriteAt(1, 10, $"${swissBankAccountTransfer.AmountStolen}, 000 has been TRANSFERRED");
            }
            else
            {
                ConsoleEx.WriteAt(8, 11, "NO TRANSFER made"); // TODO: fix placement 
            }

            pressAnyKeyControl.Show();
        }
    }
}
