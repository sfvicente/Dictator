using Dictator.Common;
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
            ConsoleEx.Clear(ConsoleColor.Red);
            ConsoleEx.WriteAt(11, 12, "REVOLUTION", ConsoleColor.White, ConsoleColor.Black);
            pressAnyKeyControl.Show();
        }
    }
}
