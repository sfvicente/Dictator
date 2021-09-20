using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the screen that is displayed when the player has no allies to ask for help
    ///     in the context of a revolution.
    /// </summary>
    public class RevolutionNoAlliesScreen : IRevolutionNoAlliesScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public RevolutionNoAlliesScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "      You're ON YOUR OWN !      ");
            pressAnyKeyControl.Show();
        }
    }
}
