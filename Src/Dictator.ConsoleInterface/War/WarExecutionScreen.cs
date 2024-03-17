using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when a player loses the war, is unable to
    ///     escape by helicopter and is executed.
    /// </summary>
    public class WarExecutionScreen : BaseScreen, IWarExecutionScreen
    {
        private readonly IPressAnyKeyControl _pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WarExecutionScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public WarExecutionScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
        {
            _pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            _consoleService.WriteAt(1, 11, "     YOU are judged to be an    ");
            _consoleService.WriteAt(1, 13, "   ENEMY of the PEOPLE and ...  ");
            _consoleService.WriteAt(1, 15, "       Summarily EXECUTED       ");
            _pressAnyKeyControl.Show();
        }
    }
}
