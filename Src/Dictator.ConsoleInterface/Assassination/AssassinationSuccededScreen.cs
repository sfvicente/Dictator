using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Assassination
{
    /// <summary>
    ///     Represents the screen that is displayed when an assassination by one of the groups against the player
    ///     succeeds.
    /// </summary>
    public class AssassinationSuccededScreen : IAssassinationSuccededScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

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
