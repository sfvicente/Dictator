using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Escape
{
    public class HelicopterEngineFailureScreen : IHelicopterEngineFailureScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public HelicopterEngineFailureScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 9, "    Helicopter ENGINE FAILURE   ");
            ConsoleEx.WriteAt(1, 11, "     YOU are judged to be an    ");
            ConsoleEx.WriteAt(1, 13, "   ENEMY of the PEOPLE and ...  ");
            ConsoleEx.WriteAt(1, 15, "       Summarily EXECUTED       ");
            pressAnyKeyControl.Show();
        }
    }
}
