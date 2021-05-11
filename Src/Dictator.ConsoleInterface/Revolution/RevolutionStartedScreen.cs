using Dictator.Common.Extensions;
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
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "   The REVOLUTION has STARTED   ");
            pressAnyKeyControl.Show();
        }
    }
}
