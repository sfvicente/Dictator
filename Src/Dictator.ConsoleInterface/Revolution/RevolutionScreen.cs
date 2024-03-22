using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the screen that is displayed when a revolution has been triggered.
    /// </summary>
    public interface IRevolutionScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show();
    }

    /// <summary>
    ///     Represents the screen that is displayed when a revolution has been triggered.
    /// </summary>
    public class RevolutionScreen : BaseScreen, IRevolutionScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RevolutionScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public RevolutionScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            _consoleService.Clear(ConsoleColor.Red);
            _consoleService.WriteAt(11, 12, "REVOLUTION", ConsoleColor.White, ConsoleColor.Black);
            pressAnyKeyControl.Show();
        }
    }
}
