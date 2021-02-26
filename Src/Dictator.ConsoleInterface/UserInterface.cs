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
        private IAccountScreen accountScreen { get; set; }
        private IPoliceReportRequestScreen policeReportRequestScreen { get; set; }
        private IPoliceReportScreen policeReportScreen { get; set; }

        public UserInterface(
            IIntroScreen intoScreen,
            IWelcomeScreen welcomeScreen,
            ITitleScreen titleScreen,
            IAccountScreen accountScreen,
            IPoliceReportRequestScreen policeReportRequestScreen,
            IPoliceReportScreen policeReportScreen)
        {
            this.introScreen = introScreen;
            this.welcomeScreen = welcomeScreen;
            this.titleScreen = titleScreen;
            this.accountScreen = accountScreen;
            this.policeReportRequestScreen = policeReportRequestScreen;
            this.policeReportScreen = policeReportScreen;
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

        public void DisplayAccountScreen()
        {
            accountScreen.Draw();
        }

        public void DisplayPoliceReportRequestScreen()
        {
            this.policeReportRequestScreen.Draw();
        }

        public void DisplayPoliceReportScreen()
        {
            this.policeReportScreen.Draw();
        }
    }
}
