using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when a player loses the war, which results
    ///     in a Leftotan victory.
    /// </summary>
    public class WarLostScreen : IWarLostScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public WarLostScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 7, "        LEFTOTAN VICTORY        ");
            pressAnyKeyControl.Show();
        }
    }
}
