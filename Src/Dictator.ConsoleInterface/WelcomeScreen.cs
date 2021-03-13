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
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(8, 4, "WELCOME to OFFICE", ConsoleColor.Black, ConsoleColor.Cyan);
            ConsoleEx.WriteAt(1, 7, "The best DICTATOR of our beloved");
            ConsoleEx.WriteAt(1, 9, "country of RITIMBA had a final  ");
            ConsoleEx.WriteAt(1, 11, "rating of 0                     ");

            // TODO: add history 
            //ConsoleEx.WriteAt(1, 9, "You can always try for 1 !");
            ConsoleEx.WriteAt(1, 14, "As this is your first attempt   ");
            ConsoleEx.WriteAt(1, 16, "you will no doubt do BETTER !   ");

            ConsoleEx.WriteAt(1, 18, "Start with a TREASURY REPORT    ");
            ConsoleEx.WriteAt(1, 20, "and a POLICE Report. (FREE)     ");
            
            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
