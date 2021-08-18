using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape
{
    public class EscapeToLeftotoScreen : IEscapeToLeftotoScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public EscapeToLeftotoScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 10, "   You have to get through the  ");
            ConsoleEx.WriteAt(1, 12, "      MOUNTAINS to LEFTOTO      ");
            pressAnyKeyControl.Show();
        }
    }
}
