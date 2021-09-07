using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when there is a threat of war with Leftoto.
    /// </summary>
    public class WarThreatScreen : IWarThreatScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public WarThreatScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
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
