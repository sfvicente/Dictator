using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when a player loses the war, which results
    ///     in a Leftotan victory.
    /// </summary>
    public class WarLostScreen : BaseScreen, IWarLostScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WarLostScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public WarLostScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            _consoleService.Clear(ConsoleColor.Gray);
            _consoleService.WriteAt(1, 7, "        LEFTOTAN VICTORY        ");
            pressAnyKeyControl.Show();
        }
    }
}
