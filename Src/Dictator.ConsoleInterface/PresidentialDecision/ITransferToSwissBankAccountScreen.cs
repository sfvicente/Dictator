using Dictator.Core;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    /// <summary>
    ///     Represents the screen that is displayed when the player transfers funds from the treasury 
    ///     to the Swiss bank account.
    /// </summary>
    public interface ITransferToSwissBankAccountScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="swissBankAccountTransfer">The details of the Swiss account transfer.</param>
        /// <param name="account">The current treasury and costs information.</param>
        public void Show(SwissBankAccountTransfer swissBankAccountTransfer, Account account);
    }
}
