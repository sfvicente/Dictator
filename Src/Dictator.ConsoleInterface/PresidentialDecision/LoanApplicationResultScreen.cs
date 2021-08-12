using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public class LoanApplicationResultScreen : ILoanApplicationResultScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public LoanApplicationResultScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show(LoanApplicationResult loanApplicationResult)
        {
            if(loanApplicationResult.IsAccepted)
            {
                ConsoleEx.WriteAt(1, 12, $"{loanApplicationResult.GroupName} will let you have");
                ConsoleEx.WriteAt(8, 14, $"{loanApplicationResult.Amount},000 DOLLARS");
            }
            else
            {
                if (loanApplicationResult.Country == Country.America)
                {
                    ConsoleEx.WriteAt(1, 12, "            \"nuts !\"            ");
                }
                else if(loanApplicationResult.Country == Country.Russia)
                {
                    ConsoleEx.WriteAt(1, 12, "             NIET !             ");
                }
            }

            pressAnyKeyControl.Show();
        }
    }
}
