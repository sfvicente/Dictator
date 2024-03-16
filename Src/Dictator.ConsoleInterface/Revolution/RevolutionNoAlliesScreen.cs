using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the screen that is displayed when the player has no allies to ask for help
    ///     in the context of a revolution.
    /// </summary>
    public class RevolutionNoAlliesScreen : BaseScreen, IRevolutionNoAlliesScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RevolutionNoAlliesScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public RevolutionNoAlliesScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            _consoleService.WriteAt(1, 12, "      You're ON YOUR OWN !      ");
            pressAnyKeyControl.Show();
        }
    }
}
