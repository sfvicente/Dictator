using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution
{
    public class RevolutionStartedScreen : IRevolutionStartedScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public RevolutionStartedScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "   The REVOLUTION has STARTED   ");
            pressAnyKeyControl.Show();
        }
    }
}
