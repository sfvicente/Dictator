using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape
{
    /// <summary>
    ///     Represents the screen that is displayed when the player successfully escapes using the helicopter.
    /// </summary>
    public class HelicopterEscapeScreen : IHelicopterEscapeScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public HelicopterEscapeScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "   You ESCAPE by HELICOPTER !   ");
            pressAnyKeyControl.Show();
        }
    }
}
