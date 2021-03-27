using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Assassination
{
    public class AssassinationFailedScreen : IAssassinationFailedScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AssassinationFailedScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 11, "         Attempt FAILED         ", ConsoleColor.Gray, ConsoleColor.Black);         
            pressAnyKeyControl.Show();
        }
    }
}
