using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.War
{
    public class WarLostScreen : IWarLostScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public WarLostScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.WriteAt(1, 7, "        LEFTOTAN VICTORY        ");
            pressAnyKeyControl.Show();
        }
    }
}
