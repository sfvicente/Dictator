using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;

namespace Dictator.ConsoleInterface.Treasury
{
    /// <summary>
    ///     Represents the screen that is displayed when a report of the treasury is required.
    /// </summary>
    public class TreasuryReportScreen : ITreasuryReportScreen
    {
        private readonly IAccountControl accountControl;
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public TreasuryReportScreen(IAccountControl accountControl, IPressAnyKeyControl pressAnyKeyControl)
        {
            this.accountControl = accountControl;
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="account">The account information regarding the treasury, monthly costs and Swiss account.</param>
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
