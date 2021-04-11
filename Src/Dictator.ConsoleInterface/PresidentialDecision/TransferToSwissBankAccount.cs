using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
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

        public void Show(int amountStolen, int treasuryBalance)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 3, "TRANSFER to a SWISS BANK ACCOUNT");

            if (amountStolen > 0)
            {
                ConsoleEx.WriteAt(1, 11, $"The TREASURY held ${treasuryBalance}, 000");
                ConsoleEx.WriteAt(1, 13, $"${amountStolen}, 000 has been TRANSFERRED");
            }
            else
            {
                ConsoleEx.WriteAt(8, 11, "NO TRANSFER made");
            }

            pressAnyKeyControl.Show();
        }
    }
}
