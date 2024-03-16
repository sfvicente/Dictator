using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Assassination
{
    /// <summary>
    ///     Represents the screen that is displayed when an assassination by one of the groups against the player
    ///     succeeds.
    /// </summary>
    public class AssassinationSuccededScreen : BaseScreen, IAssassinationSuccededScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AssassinationSuccededScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public AssassinationSuccededScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            _consoleService.Clear(ConsoleColor.Gray);
            _consoleService.WriteAt(1, 11, "          You're DEAD !         ", ConsoleColor.Gray, ConsoleColor.Black);
            pressAnyKeyControl.Show();
        }
    }
}
