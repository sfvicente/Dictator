using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using Dictator.Core.Services;
using System;

namespace Dictator.ConsoleInterface.Treasury
{
    public class TreasuryReportScreen : ITreasuryReportScreen
    {
        private readonly IAccountControl accountControl;
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public TreasuryReportScreen(IAccountControl accountControl, IPressAnyKeyControl pressAnyKeyControl)
        {
            this.accountControl = accountControl;
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show(Account account)
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Green);
            ConsoleEx.Clear('$');
            ConsoleEx.WriteAt(2, 4, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(2, 5, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(2, 6, "                              ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(8, 9, "TREASURY REPORT", ConsoleColor.White, ConsoleColor.Black);
            accountControl.Show(account);
            pressAnyKeyControl.Show();
        }
    }
}
