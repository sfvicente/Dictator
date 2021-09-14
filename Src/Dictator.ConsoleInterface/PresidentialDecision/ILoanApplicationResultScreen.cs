using Dictator.Core;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public interface ILoanApplicationResultScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="loanApplicationResult">The load application result to be displayed on screen.</param>
        public void Show(LoanApplicationResult loanApplicationResult);
    }
}
