using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.War
{
    public class WarExecutionScreen : IWarExecutionScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public WarExecutionScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 11, "     YOU are judged to be an    ");
            ConsoleEx.WriteAt(1, 13, "   ENEMY of the PEOPLE and ...  ");
            ConsoleEx.WriteAt(1, 15, "       Summarily EXECUTED       ");
            pressAnyKeyControl.Show();
        }
    }
}
