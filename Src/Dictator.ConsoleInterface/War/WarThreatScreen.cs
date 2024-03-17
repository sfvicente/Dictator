using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when there is a threat of war with Leftoto.
    /// </summary>
    public class WarThreatScreen : BaseScreen, IWarThreatScreen
    {
        private readonly IPressAnyKeyControl _pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WarThreatScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public WarThreatScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
        {
            _pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            _consoleService.WriteAt(1, 6, "   THREAT of WAR with LEFTOTO   ");
            _consoleService.WriteAt(1, 11, "   Your POPULARITY in RITIMBA   ");
            _consoleService.WriteAt(1, 12, "            will RISE           ");
            _pressAnyKeyControl.Show();
        }
    }
}
