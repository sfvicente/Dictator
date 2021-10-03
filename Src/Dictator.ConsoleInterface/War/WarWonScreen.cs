using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when a player wins the war against Leftoto.
    /// </summary>
    public class WarWonScreen : IWarWonScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public WarWonScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 11, "        LEFTOTANS ROUTED        ");
            pressAnyKeyControl.Show();
        }
    }
}
