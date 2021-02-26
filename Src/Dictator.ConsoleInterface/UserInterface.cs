using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class UserInterface : IUserInterface
    {
        private IIntroScreen introScreen { get; set; }
        private IWelcomeScreen welcomeScreen { get; set; }
        private ITitleScreen titleScreen { get; set; }

        public UserInterface(
            IIntroScreen intoScreen,
            IWelcomeScreen welcomeScreen,
            ITitleScreen titleScreen)
        {
            this.introScreen = introScreen;
            this.welcomeScreen = welcomeScreen;
            this.titleScreen = titleScreen;
        }

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
            titleScreen.Draw();
        }

    }
}
