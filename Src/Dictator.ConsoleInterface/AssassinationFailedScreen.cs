using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
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
            ConsoleEx.WriteAt(1, 11, "          You're DEAD !         ", ConsoleColor.Gray, ConsoleColor.Black);
            
            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
