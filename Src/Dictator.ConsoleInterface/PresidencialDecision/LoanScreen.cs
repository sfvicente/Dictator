using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class LoanScreen : ILoanScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public LoanScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            pressAnyKeyControl.Show();
        }
    }
}
