using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class WelcomeScreen : IWelcomeScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public WelcomeScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray);
            ConsoleEx.WriteAt(24, 3, "        WELCOME to OFFICE       ");
            ConsoleEx.WriteAt(24, 5, "The best DICTATOR of our beloved");
            ConsoleEx.WriteAt(24, 6, "country of RITIMBA had a final  ");
            ConsoleEx.WriteAt(24, 7, "rating of 0                     ");

            // TODO: add history 
            //ConsoleEx.WriteAt(24, 9, "You can always try for 1 !");
            ConsoleEx.WriteAt(24, 9, "As this is your first attempt   ");
            ConsoleEx.WriteAt(24, 10, "you will no doubt do BETTER !   ");

            ConsoleEx.WriteAt(24, 11, "Start with a TREASURY REPORT    ");
            ConsoleEx.WriteAt(24, 12, "and a POLICE Report. (FREE)     ");
            
            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
