using Dictator.Core;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
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
