using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Start
{
    public class WelcomeScreen : IWelcomeScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public WelcomeScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show(int highscore)
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(8, 4, "WELCOME to OFFICE", ConsoleColor.Black, ConsoleColor.Cyan);
            ConsoleEx.WriteAt(1, 7, "The best DICTATOR of our beloved");
            ConsoleEx.WriteAt(1, 9, "country of RITIMBA had a final  ");
            ConsoleEx.WriteAt(1, 11, $"rating of {highscore}");

            if (highscore > 0)
            {
                ConsoleEx.WriteAt(1, 14, $"You can always try for {highscore + 1} !");
            }
            else
            {
                ConsoleEx.WriteAt(1, 14, "As this is your first attempt   ");
                ConsoleEx.WriteAt(1, 16, "you will no doubt do BETTER !   ");
            }

            ConsoleEx.WriteAt(1, 18, "Start with a TREASURY REPORT    ");
            ConsoleEx.WriteAt(1, 20, "and a POLICE Report. (FREE)     ");          
            pressAnyKeyControl.Show();
        }
    }
}
