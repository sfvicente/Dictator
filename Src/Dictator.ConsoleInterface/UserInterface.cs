using Dictator.ConsoleInterface.Assassination;
using Dictator.ConsoleInterface.Escape;
using Dictator.ConsoleInterface.PoliceReport;
using Dictator.ConsoleInterface.Revolution;
using Dictator.ConsoleInterface.Treasury;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class UserInterface : IUserInterface
    {
        private readonly IIntroScreen introScreen;
        private readonly IWelcomeScreen welcomeScreen;
        private readonly ITitleScreen titleScreen;
        private readonly ITreasuryReportScreen treasuryReportScreen;
        private readonly IBankruptcyScreen bankruptcyScreen;
        private readonly IPoliceReportRequestDialog policeReportRequestScreen;
        private readonly IPoliceReportScreen policeReportScreen;
        private readonly IAudienceScreen audienceScreen;
        private readonly IAudienceDecisionDialog audienceDecisionDialog;
        private readonly IAdviceRequestDialog adviceRequestDialog;
        private readonly IAdviceScreen adviceScreen;
        private readonly INewsflashScreen newsflashScreen;
        private readonly IMonthScreen monthScreen;
        private readonly IPresidentialDecisionMainDialog presidentialDecisionMainDialog;
        private readonly IPresidentialDecisionSubDialog presidentialDecisionSubDialog;
        private readonly IPresidentialDecisionActionDialog presidentialDecisionActionDialog;
        private readonly ILoanScreen loanScreen;
        private readonly IAssassinationScreen assassinationScreen;
        private readonly IAssassinationSuccededScreen assassinationSuccededScreen;
        private readonly IAssassinationFailedScreen assassinationFailedScreen;
        private readonly IRevolutionScreen revolutionScreen;
        private readonly IEscapeAttemptDialog escapeAttemptDialog;
        private readonly IEscapeByHelicopterScreen escapeByHelicopterScreen;
        private readonly IEscapeByHelicopterFailScreen escapeByHelicopterFailScreen;
        private readonly IEscapeToLeftotoScreen escapeToLeftotoScreen;
        private readonly IEndScreen endScreen;

        public UserInterface(
            IIntroScreen introScreen,
            IWelcomeScreen welcomeScreen,
            ITitleScreen titleScreen,
            ITreasuryReportScreen treasuryReportScreen,
            IBankruptcyScreen bankruptcyScreen,
            IPoliceReportRequestDialog policeReportRequestScreen,
            IPoliceReportScreen policeReportScreen,
            IAudienceScreen audienceScreen,
            IAudienceDecisionDialog audienceDecisionDialog,
            IAdviceRequestDialog adviceRequestDialog,
            IAdviceScreen adviceScreen,
            INewsflashScreen newsflashScreen,
            IMonthScreen monthScreen,
            IPresidentialDecisionMainDialog presidentialDecisionMainDialog,
            IPresidentialDecisionSubDialog presidentialDecisionSubDialog,
            IPresidentialDecisionActionDialog presidentialDecisionActionDialog,
            ILoanScreen loanScreen,
            IAssassinationScreen assassinationScreen,
            IAssassinationSuccededScreen assassinationSuccededScreen,
            IAssassinationFailedScreen assassinationFailedScreen,
            IRevolutionScreen revolutionScreen,
            IEscapeAttemptDialog escapeAttemptDialog,
            IEscapeByHelicopterScreen escapeByHelicopterScreen,
            IEscapeByHelicopterFailScreen escapeByHelicopterFailScreen,
            IEscapeToLeftotoScreen escapeToLeftotoScreen,
            IEndScreen endScreen)
        {
            this.introScreen = introScreen;
            this.welcomeScreen = welcomeScreen;
            this.titleScreen = titleScreen;
            this.treasuryReportScreen = treasuryReportScreen;
            this.policeReportRequestScreen = policeReportRequestScreen;
            this.policeReportScreen = policeReportScreen;
            this.audienceScreen = audienceScreen;
            this.audienceDecisionDialog = audienceDecisionDialog;
            this.adviceRequestDialog = adviceRequestDialog;
            this.adviceScreen = adviceScreen;
            this.bankruptcyScreen = bankruptcyScreen;
            this.newsflashScreen = newsflashScreen;
            this.monthScreen = monthScreen;
            this.presidentialDecisionMainDialog = presidentialDecisionMainDialog;
            this.presidentialDecisionSubDialog = presidentialDecisionSubDialog;
            this.presidentialDecisionActionDialog = presidentialDecisionActionDialog;
            this.loanScreen = loanScreen;
            this.assassinationScreen = assassinationScreen;
            this.assassinationSuccededScreen = assassinationSuccededScreen;
            this.assassinationFailedScreen = assassinationFailedScreen;
            this.revolutionScreen = revolutionScreen;
            this.escapeAttemptDialog = escapeAttemptDialog;
            this.escapeByHelicopterScreen = escapeByHelicopterScreen;
            this.escapeByHelicopterFailScreen = escapeByHelicopterFailScreen;
            this.escapeToLeftotoScreen = escapeToLeftotoScreen;
            this.endScreen = endScreen;
        }

        public void Initialise()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
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
            titleScreen.Show();
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

        public DialogResult DisplayAudienceDecisionDialog()
        {
            return audienceDecisionDialog.Show();
        }

        public DialogResult DisplayAdviceRequestDialog()
        {
            return adviceRequestDialog.Show();
        }

        public void DisplayAdviceScreen()
        {
            adviceScreen.Show();
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

        public DecisionType DisplayPresidentialDecisionMainDialog()
        {
            return presidentialDecisionMainDialog.Show();
        }

        public int DisplayPresidentialDecisionSubDialog(DecisionType decisionType)
        {
            return presidentialDecisionSubDialog.Show(decisionType);
        }

        public void DisplayPresidentialDecisionActionDialog()
        {
            presidentialDecisionActionDialog.Show();
        }

        public void DisplayLoanScreen()
        {
            loanScreen.Show();
        }

        public void DisplayAssassinationAttempt()
        {
            assassinationScreen.Show();
        }

        public void DisplayAssassinationFailedScreen()
        {
            assassinationFailedScreen.Show();
        }

        public void DisplayAssassinationSuccededScreen()
        {
            assassinationSuccededScreen.Show();
        }

        public void DisplayRevolutionScreen()
        {
            revolutionScreen.Show();
        }

        public DialogResult DisplayEscapeAttemptDialog()
        {
            return escapeAttemptDialog.Show();
        }

        public void DisplayEscapeByHelicopterScreen()
        {
            escapeByHelicopterScreen.Show();
        }

        public void DisplayEscapeByHelicopterFailScreen()
        {
            escapeByHelicopterFailScreen.Show();
        }

        public void DisplayEscapeToLeftotoScreen()
        {
            escapeToLeftotoScreen.Show();
        }

        public void DisplayEndScreen()
        {
            endScreen.Show();
        }
    }
}
