using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Escape
{
    public class HelicopterWontStartScreen : IHelicopterWontStartScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public HelicopterWontStartScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "  The HELICOPTER won't START !  ");
            pressAnyKeyControl.Show();
        }
    }
}
