using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Revolution
{
    public class RevolutionScreen : IRevolutionScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public RevolutionScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "           REVOLUTION           ");
            pressAnyKeyControl.Show();
        }
    }
}
