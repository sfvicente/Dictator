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
        private ITreasuryReportScreen treasuryReportScreen;
        private IPoliceReportRequestDialog policeReportRequestScreen;
        private IPoliceReportScreen policeReportScreen;
        private IAudienceScreen audienceScreen;
        private readonly IAdviceRequestScreen adviceRequestScreen;
        private readonly IBankruptcyScreen bankruptcyScreen;
        private readonly INewsflashScreen newsflashScreen;
        private readonly IMonthScreen monthScreen;
        private readonly IDecisionMainDialog decisionMainDialog;
        private readonly IEscapeByHelicopterScreen escapeByHelicopterScreen;
        private readonly IEscapeByHelicopterFailScreen escapeByHelicopterFailScreen;
        private readonly IEndScreen endScreen;

        public UserInterface(
            IIntroScreen introScreen,
            IWelcomeScreen welcomeScreen,
            ITitleScreen titleScreen,
            ITreasuryReportScreen treasuryReportScreen,
            IPoliceReportRequestDialog policeReportRequestScreen,
            IPoliceReportScreen policeReportScreen,
            IAudienceScreen audienceScreen,
            IAdviceRequestScreen adviceRequestScreen,
            IBankruptcyScreen bankruptcyScreen,
            INewsflashScreen newsflashScreen,
            IMonthScreen monthScreen,
            IDecisionMainDialog decisionMainDialog,
            IEscapeByHelicopterScreen escapeByHelicopterScreen,
            IEscapeByHelicopterFailScreen escapeByHelicopterFailScreen,
            IEndScreen endScreen)
        {
            this.introScreen = introScreen;
            this.welcomeScreen = welcomeScreen;
            this.titleScreen = titleScreen;
            this.treasuryReportScreen = treasuryReportScreen;
            this.policeReportRequestScreen = policeReportRequestScreen;
            this.policeReportScreen = policeReportScreen;
            this.audienceScreen = audienceScreen;
            this.adviceRequestScreen = adviceRequestScreen;
            this.bankruptcyScreen = bankruptcyScreen;
            this.newsflashScreen = newsflashScreen;
            this.monthScreen = monthScreen;
            this.decisionMainDialog = decisionMainDialog;
            this.escapeByHelicopterScreen = escapeByHelicopterScreen;
            this.escapeByHelicopterFailScreen = escapeByHelicopterFailScreen;
            this.endScreen = endScreen;
        }

        public void DisplayIntroScreen()
        {
            introScreen.Show();
        }

        public void DisplayWelcomeScreen()
        {
            welcomeScreen.Show();
        }

        public void DisplayTitleScreen()
        {
            titleScreen.Draw();
        }

        public void DisplayAccountScreen()
        {
            treasuryReportScreen.Show();
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

        public void DisplayAssassinationAttempt()
        {

        }

        public void DisplayEscapeByHelicopterScreen()
        {
            escapeByHelicopterScreen.Show();
        }

        public void DisplayEscapeByHelicopterFailScreen()
        {
            escapeByHelicopterFailScreen.Show();
        }

        public void DisplayEndScreen()
        {
            endScreen.Show();
        }
    }
}
