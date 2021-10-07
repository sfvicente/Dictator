using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Assassination
{
    /// <summary>
    ///     Represents the screen that is displayed when an assassination by one of the groups against the player
    ///     fails.
    /// </summary>
    public class AssassinationFailedScreen : IAssassinationFailedScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AssassinationFailedScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 11, "         Attempt FAILED         ", ConsoleColor.Gray, ConsoleColor.Black);         
            pressAnyKeyControl.Show();
        }
    }
}
