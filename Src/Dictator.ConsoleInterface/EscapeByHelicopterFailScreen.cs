using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class EscapeByHelicopterFailScreen : IEscapeByHelicopterFailScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public EscapeByHelicopterFailScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "  The HELICOPTER won't START !  ");
            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
