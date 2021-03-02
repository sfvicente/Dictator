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
        private IPoliceReportRequestDialog policeReportRequestScreen;
        private IPoliceReportScreen policeReportScreen;
        private IRequestScreen requestScreen;
        private readonly IBankruptcyScreen bankruptcyScreen;
        private readonly INewsflashScreen newsflashScreen;
        private readonly IMonthScreen monthScreen;

        public UserInterface(
            IIntroScreen introScreen,
            IWelcomeScreen welcomeScreen,
            ITitleScreen titleScreen,
            IAccountScreen accountScreen,
            IPoliceReportRequestDialog policeReportRequestScreen,
            IPoliceReportScreen policeReportScreen,
            IRequestScreen requestScreen,
            IBankruptcyScreen bankruptcyScreen,
            INewsflashScreen newsflashScreen,
            IMonthScreen monthScreen)
        {
            this.introScreen = introScreen;
            this.welcomeScreen = welcomeScreen;
            this.titleScreen = titleScreen;
            this.accountScreen = accountScreen;
            this.policeReportRequestScreen = policeReportRequestScreen;
            this.policeReportScreen = policeReportScreen;
            this.requestScreen = requestScreen;
            this.bankruptcyScreen = bankruptcyScreen;
            this.newsflashScreen = newsflashScreen;
            this.monthScreen = monthScreen;
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

        public bool DisplayPoliceReportRequestDialog()
        {
            return policeReportRequestScreen.Show();
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

        public void DisplayNewsScreen()
        {
            newsflashScreen.Show();
        }

        public void DisplayMonthScreen()
        {
            monthScreen.Show();
        }
    }
}
