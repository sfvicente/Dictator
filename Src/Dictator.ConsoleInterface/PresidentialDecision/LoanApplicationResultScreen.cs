using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

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
                string groupName = string.Empty;

                if (loanApplicationResult.Country == Country.America)
                {
                    groupName = "Americans";
                }
                else if (loanApplicationResult.Country == Country.Russia)
                {
                    groupName = "Russians";
                }

                ConsoleEx.WriteAt(1, 12, $"{groupName} will let you have");
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
