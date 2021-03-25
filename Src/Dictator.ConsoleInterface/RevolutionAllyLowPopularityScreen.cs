using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class RevolutionAllyLowPopularityScreen : IRevolutionAllyLowPopularityScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public RevolutionAllyLowPopularityScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
