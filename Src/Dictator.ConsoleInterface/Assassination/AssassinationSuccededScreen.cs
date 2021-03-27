using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Assassination
{
    public class AssassinationSuccededScreen : IAssassinationSuccededScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AssassinationSuccededScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 11, "          You're DEAD !         ", ConsoleColor.Gray, ConsoleColor.Black);
            pressAnyKeyControl.Show();
        }
    }
}
