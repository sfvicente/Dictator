using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class UserInterface : IUserInterface
    {
        private IIntroScreen introScreen;
        private IWelcomeScreen welcomeScreen;
        private ITitleScreen titleScreen;
        private IAccountScreen accountScreen;
        private IPoliceReportRequestScreen policeReportRequestScreen;
        private IPoliceReportScreen policeReportScreen;
        private IRequestScreen requestScreen;
        private readonly IBankruptcyScreen bankruptcyScreen;

        public UserInterface(
            IIntroScreen introScreen,
            IWelcomeScreen welcomeScreen,
            ITitleScreen titleScreen,
            IAccountScreen accountScreen,
            IPoliceReportRequestScreen policeReportRequestScreen,
            IPoliceReportScreen policeReportScreen,
            IRequestScreen requestScreen,
            IBankruptcyScreen bankruptcyScreen)
        {
            this.introScreen = introScreen;
            this.welcomeScreen = welcomeScreen;
            this.titleScreen = titleScreen;
            this.accountScreen = accountScreen;
            this.policeReportRequestScreen = policeReportRequestScreen;
            this.policeReportScreen = policeReportScreen;
            this.requestScreen = requestScreen;
            this.bankruptcyScreen = bankruptcyScreen;
        }

        public void DisplayIntroScreen()
        {
            IntroScreen screen = new IntroScreen();

            screen.Show();
        }

        public void DisplayWelcomeScreen()
        {
            WelcomeScreen screen = new WelcomeScreen();

            screen.Show();

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

        public void DisplayRequestScreen()
        {
            this.requestScreen.Draw();
        }

        public void DisplayBankuptcyScreen()
        {
            bankruptcyScreen.Show();
        }
    }
}
