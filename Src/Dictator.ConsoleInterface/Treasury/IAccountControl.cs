using Dictator.Core.Models;

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
}
