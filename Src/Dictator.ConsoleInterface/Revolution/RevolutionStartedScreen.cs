using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Revolution
{
    public class RevolutionStartedScreen : IRevolutionStartedScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public RevolutionStartedScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
