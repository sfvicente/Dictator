using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when Leftoto invades and initiates the war
    ///     against the republic of Ritimban.
    /// </summary>
    public class WarLeftotoInvadesScreen : BaseScreen, IWarLeftotoInvadesScreen
    {
        private readonly IPressAnyKeyControl _pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WarLeftotoInvadesScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public WarLeftotoInvadesScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
        {
            _pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="warStats">The attributes of a scenario of war between the Ritimba republic and Leftoto.</param>
        public void Show(WarStats warStats)
        {
            _consoleService.Clear(ConsoleColor.Red);
            _consoleService.WriteAt(7, 8, " LEFTOTO  INVADES ", ConsoleColor.Black, ConsoleColor.White);
            _consoleService.WriteAt(1, 12, $"     Ritimban Strength is {warStats.RitimbanStrength}    ", ConsoleColor.Red, ConsoleColor.Gray);
            _consoleService.WriteAt(1, 14, $"     Leftotan Strength is {warStats.LeftotanStrength}    ", ConsoleColor.Red, ConsoleColor.Gray);
            _consoleService.WriteAt(6, 18, "A SHORT DECISIVE WAR", ConsoleColor.White, ConsoleColor.Black);
            _pressAnyKeyControl.Show();
        }
    }
}
