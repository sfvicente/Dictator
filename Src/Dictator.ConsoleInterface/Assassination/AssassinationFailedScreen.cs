using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

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
