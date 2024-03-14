using Dictator.Core.Models;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    /// <summary>
    ///     Represents the screen that is displayed when the result of an application 
    ///     for monetary foreign aid that a player is determined.
    /// </summary>
    public interface ILoanApplicationResultScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="loanApplicationResult">The load application result to be displayed on screen.</param>
        public void Show(LoanApplicationResult loanApplicationResult);
    }
}
