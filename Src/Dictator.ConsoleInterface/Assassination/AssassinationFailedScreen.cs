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

        /// <summary>
        ///     Initializes a new instance of the <see cref="AssassinationFailedScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
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
