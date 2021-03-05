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
        private IAudienceScreen audienceScreen;
        private readonly IAdviceRequestScreen adviceRequestScreen;
        private readonly IBankruptcyScreen bankruptcyScreen;
        private readonly INewsflashScreen newsflashScreen;
        private readonly IMonthScreen monthScreen;
        private readonly IDecisionMainDialog decisionMainDialog;
        private readonly IEndScreen endScreen;

        public UserInterface(
            IIntroScreen introScreen,
            IWelcomeScreen welcomeScreen,
            ITitleScreen titleScreen,
            IAccountScreen accountScreen,
            IPoliceReportRequestDialog policeReportRequestScreen,
            IPoliceReportScreen policeReportScreen,
            IAudienceScreen audienceScreen,
            IAdviceRequestScreen adviceRequestScreen,
            IBankruptcyScreen bankruptcyScreen,
            INewsflashScreen newsflashScreen,
            IMonthScreen monthScreen,
            IDecisionMainDialog decisionMainDialog,
            IEndScreen endScreen)
        {
            this.introScreen = introScreen;
            this.welcomeScreen = welcomeScreen;
            this.titleScreen = titleScreen;
            this.accountScreen = accountScreen;
            this.policeReportRequestScreen = policeReportRequestScreen;
            this.policeReportScreen = policeReportScreen;
            this.audienceScreen = audienceScreen;
            this.adviceRequestScreen = adviceRequestScreen;
            this.bankruptcyScreen = bankruptcyScreen;
            this.newsflashScreen = newsflashScreen;
            this.monthScreen = monthScreen;
            this.decisionMainDialog = decisionMainDialog;
            this.endScreen = endScreen;
        }

        public void DisplayIntroScreen()
        {
            introScreen.Show();
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
            policeReportScreen.Show();
        }

        public void DisplayAudienceScreen()
        {
            audienceScreen.Show();
        }

        public void DisplayAdviceScreen()
        {
            adviceRequestScreen.Show();
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

        public void DisplayMainDecisionDialog()
        {
            decisionMainDialog.Show();
        }


        public void DisplayEndScreen()
        {
            endScreen.Show();
        }
    }
}
