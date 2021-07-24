using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution
{
    public class RevolutionAllyLowPopularityScreen : IRevolutionAllyLowPopularityScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public RevolutionAllyLowPopularityScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "      You must be JOKING !      ");
            pressAnyKeyControl.Show();
        }
    }
}
