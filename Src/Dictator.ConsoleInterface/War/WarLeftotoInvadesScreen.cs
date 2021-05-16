using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.War
{
    public class WarLeftotoInvadesScreen : IWarLeftotoInvadesScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public WarLeftotoInvadesScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show(WarStats warStats)
        {
            ConsoleEx.WriteAt(1, 8, "        LEFTOTO  INVADES        ");
            ConsoleEx.WriteAt(1, 12, $"     Ritimban Strength is {warStats.RitimbanStrength}    ");
            ConsoleEx.WriteAt(1, 14, $"     Leftotan Strength is {warStats.LeftotanStrength}    ");
            ConsoleEx.WriteAt(1, 18, "      A SHORT DECISIVE WAR      ");
            pressAnyKeyControl.Show();
        }
    }
}
