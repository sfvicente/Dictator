using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class UserInterface
    {
        public void DisplayIntroScreen()
        {
            IntroScreen screen = new IntroScreen();
        }

        public void DisplayWelcomeScreen()
        {
            WelcomeScreen screen = new WelcomeScreen();

        }

        public void DisplayTitleScreen()
        {
            TitleScreen screen = new TitleScreen();

            screen.Draw();
        }

    }
}
