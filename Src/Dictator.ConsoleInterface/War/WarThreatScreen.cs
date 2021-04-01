using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.War
{
    public class WarThreatScreen : IWarThreatScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public WarThreatScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 6, "   THREAT of WAR with LEFTOTO   ");
            ConsoleEx.WriteAt(1, 11, "   Your POPULARITY in RITIMBA   ");
            ConsoleEx.WriteAt(1, 12, "            will RISE           ");
            pressAnyKeyControl.Show();
        }
    }
}
