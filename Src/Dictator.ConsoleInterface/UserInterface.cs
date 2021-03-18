﻿using Dictator.Core;
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
        private readonly IAudienceDecisionDialog audienceDecisionDialog;
        private readonly IAdviceRequestDialog adviceRequestDialog;
        private readonly IAdviceScreen adviceScreen;
        private readonly IBankruptcyScreen bankruptcyScreen;
        private readonly INewsflashScreen newsflashScreen;
        private readonly IMonthScreen monthScreen;
        private readonly IPresidentialDecisionMainDialog presidentialDecisionMainDialog;
        private readonly IPresidentialDecisionSubDialog presidentialDecisionSubDialog;
        private readonly IPresidentialDecisionActionDialog presidentialDecisionActionDialog;
        private readonly ILoanScreen loanScreen;
        private readonly IAssassinationSuccededScreen assassinationSuccededScreen;
        private readonly IAssassinationFailedScreen assassinationFailedScreen;
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
            IPoliceReportRequestDialog policeReportRequestScreen,
            IPoliceReportScreen policeReportScreen,
            IAudienceScreen audienceScreen,
            IAudienceDecisionDialog audienceDecisionDialog,
            IAdviceRequestDialog adviceRequestDialog,
            IAdviceScreen adviceScreen,
            IBankruptcyScreen bankruptcyScreen,
            INewsflashScreen newsflashScreen,
            IMonthScreen monthScreen,
            IPresidentialDecisionMainDialog presidentialDecisionMainDialog,
            IPresidentialDecisionSubDialog presidentialDecisionSubDialog,
            IPresidentialDecisionActionDialog presidentialDecisionActionDialog,
            ILoanScreen loanScreen,
            IAssassinationSuccededScreen assassinationSuccededScreen,
            IAssassinationFailedScreen assassinationFailedScreen,
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
            this.assassinationSuccededScreen = assassinationSuccededScreen;
            this.assassinationFailedScreen = assassinationFailedScreen;
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

        public void DisplayPresidentialDecisionSubDialog(DecisionType decisionType)
        {
            presidentialDecisionSubDialog.Show(decisionType);
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

        }

        public void DisplayAssassinationFailedScreen()
        {
            assassinationFailedScreen.Show();
        }

        public void DisplayAssassinationSuccededScreen()
        {
            assassinationSuccededScreen.Show();
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
