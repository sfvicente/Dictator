using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when Leftoto invades and initiates the war
    ///     against the republic of Ritimban.
    /// </summary>
    public class WarLeftotoInvadesScreen : IWarLeftotoInvadesScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public WarLeftotoInvadesScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="warStats">The attributes of a scenario of war between the Ritimba republic and Leftoto.</param>
        public void Show(WarStats warStats)
        {
            ConsoleEx.Clear(ConsoleColor.Red);
            ConsoleEx.WriteAt(7, 8, " LEFTOTO  INVADES ", ConsoleColor.Black, ConsoleColor.White);
            ConsoleEx.WriteAt(1, 12, $"     Ritimban Strength is {warStats.RitimbanStrength}    ", ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 14, $"     Leftotan Strength is {warStats.LeftotanStrength}    ", ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleEx.WriteAt(6, 18, "A SHORT DECISIVE WAR", ConsoleColor.White, ConsoleColor.Black);
            pressAnyKeyControl.Show();
        }
    }
}
