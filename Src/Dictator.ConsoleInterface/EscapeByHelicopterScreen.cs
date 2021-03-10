using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class EscapeByHelicopterScreen : IEscapeByHelicopterScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public EscapeByHelicopterScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(24, 12, "   You ESCAPE by HELICOPTER !   ");
            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
