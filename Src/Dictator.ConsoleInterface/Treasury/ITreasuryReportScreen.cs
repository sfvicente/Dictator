using Dictator.Core.Models;

namespace Dictator.ConsoleInterface.Treasury
{
    /// <summary>
    ///     Represents the screen that is displayed when a report of the treasury is required.
    /// </summary>
    public interface ITreasuryReportScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="account">The account information regarding the treasury, monthly costs and Swiss account.</param>
        public void Show(Account account);
    }
}
