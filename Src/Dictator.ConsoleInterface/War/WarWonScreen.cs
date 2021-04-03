using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.War
{
    public class WarWonScreen : IWarWonScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public WarWonScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.WriteAt(1, 11, "        LEFTOTANS ROUTED        ");
            pressAnyKeyControl.Show();
        }
    }
}
