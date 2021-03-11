using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
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
            ConsoleEx.WriteAt(24, 10, "   You have to get through the  ");
            ConsoleEx.WriteAt(24, 12, "      MOUNTAINS to LEFTOTO      ");
            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
