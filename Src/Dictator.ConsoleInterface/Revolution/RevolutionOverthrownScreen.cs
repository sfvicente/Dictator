using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the screen that is displayed when the player is overthrown in the context of a revolution.
    /// </summary>
    public class RevolutionOverthrownScreen : IRevolutionOverthrownScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public RevolutionOverthrownScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 10, "    You have been OVERTHROWN    ");
            ConsoleEx.WriteAt(1, 12, "         and LIQUIDATED         ");
            pressAnyKeyControl.Show();
        }
    }
}
