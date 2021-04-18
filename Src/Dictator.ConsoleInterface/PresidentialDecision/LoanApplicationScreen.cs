using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public class LoanApplicationScreen : ILoanApplicationScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public LoanApplicationScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            pressAnyKeyControl.Show();
        }
    }
}
