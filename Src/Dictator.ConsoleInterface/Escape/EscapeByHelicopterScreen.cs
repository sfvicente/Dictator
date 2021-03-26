using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Escape
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
            ConsoleEx.WriteAt(1, 12, "   You ESCAPE by HELICOPTER !   ");
            pressAnyKeyControl.Show();
        }
    }
}
