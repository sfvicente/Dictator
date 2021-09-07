using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape
{
    /// <summary>
    ///     Represents the screen that is displayed when the player attempts escape using the helicopter
    ///     but it won't start.
    /// </summary>
    public class HelicopterWontStartScreen : IHelicopterWontStartScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public HelicopterWontStartScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "  The HELICOPTER won't START !  ");
            pressAnyKeyControl.Show();
        }
    }
}
