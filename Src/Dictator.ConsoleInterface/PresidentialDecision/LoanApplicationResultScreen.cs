using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    /// <summary>
    ///     Represents the screen that is displayed when the result of an application 
    ///     for monetary foreign aid that a player is determined.
    /// </summary>
    public class LoanApplicationResultScreen : ILoanApplicationResultScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public LoanApplicationResultScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="loanApplicationResult">The load application result to be displayed on screen.</param>
        public void Show(LoanApplicationResult loanApplicationResult)
        {
            if(loanApplicationResult.IsAccepted)
            {
                ConsoleEx.WriteAt(1, 12, $"{loanApplicationResult.GroupName} will let you have");
                ConsoleEx.WriteAt(8, 14, $"{loanApplicationResult.Amount},000 DOLLARS");
            }
            else
            {
                if (loanApplicationResult.Country == LenderCountry.America)
                {
                    ConsoleEx.WriteAt(1, 12, "            \"nuts !\"            ");
                }
                else if(loanApplicationResult.Country == LenderCountry.Russia)
                {
                    ConsoleEx.WriteAt(1, 12, "             NIET !             ");
                }
            }

            pressAnyKeyControl.Show();
        }
    }
}
