using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Assassination
{
    /// <summary>
    ///     Represents the screen that is displayed when an assassination by one of the groups against the player
    ///     succeeds.
    /// </summary>
    public class AssassinationSuccededScreen : IAssassinationSuccededScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AssassinationSuccededScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public AssassinationSuccededScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 11, "          You're DEAD !         ", ConsoleColor.Gray, ConsoleColor.Black);
            pressAnyKeyControl.Show();
        }
    }
}
