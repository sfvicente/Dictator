using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Escape
{
    public class GuerillasCelebratingScreen : IGuerillasCelebratingScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public GuerillasCelebratingScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 11, " The GUERILLAS are CELEBRATING  ");
            pressAnyKeyControl.Show();
        }
    }
}
