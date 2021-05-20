using Dictator.ConsoleInterface.Advice;
using Dictator.ConsoleInterface.Assassination;
using Dictator.ConsoleInterface.Audience;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.End;
using Dictator.ConsoleInterface.Escape;
using Dictator.ConsoleInterface.Month;
using Dictator.ConsoleInterface.News;
using Dictator.ConsoleInterface.Reporting;
using Dictator.ConsoleInterface.PresidentialDecision;
using Dictator.ConsoleInterface.Revolution;
using Dictator.ConsoleInterface.Start;
using Dictator.ConsoleInterface.Treasury;
using Dictator.ConsoleInterface.War;
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
        private readonly ILoanApplicationScreen loanApplicationScreen;
        private readonly ILoanApplicationResultScreen loanApplicationResultScreen;
        private readonly ITransferToSwissBankAccountScreen transferToSwissBankAccountScreen;
        private readonly IAssassinationScreen assassinationScreen;
        private readonly IAssassinationSuccededScreen assassinationSuccededScreen;
        private readonly IAssassinationFailedScreen assassinationFailedScreen;
        private readonly IRevolutionScreen revolutionScreen;
        private readonly IRevolutionStartedScreen revolutionStartedScreen;
        private readonly IRevolutionAskForHelpDialog revolutionAskForHelpDialog;
        private readonly IRevolutionNoAlliesScreen revolutionNoAlliesScreen;
        private readonly IRevolutionCrushedDialog revolutionCrushedDialog;
        private readonly IRevolutionOverthrownScreen revolutionOverthrownScreen;
        private readonly IWarThreatScreen warThreatScreen;
        private readonly IWarLeftotoInvadesScreen leftotoInvadesScreen;
        private readonly IWarLostScreen warLostScreen;
        private readonly IWarWonScreen warWonScreen;
        private readonly IEscapeAttemptDialog escapeAttemptDialog;
        private readonly IHelicopterEscapeScreen helicopterEscapeScreen;
        private readonly IHelicopterWontStartScreen helicopterWontStartScreen;
        private readonly IEscapeToLeftotoScreen escapeToLeftotoScreen;
        private readonly IGuerillasCelebratingScreen guerillasCelebratingScreen;
        private readonly IGuerillasMissedScreen guerillasMissedScreen;
        private readonly IHelicopterEngineFailureScreen helicopterEngineFailureScreen;
        private readonly IWarExecutionScreen warExecutionScreen;
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
            ILoanApplicationScreen loanApplicationScreen,
            ILoanApplicationResultScreen loanApplicationResultScreen,
            ITransferToSwissBankAccountScreen transferToSwissBankAccountScreen,
            IAssassinationScreen assassinationScreen,
            IAssassinationSuccededScreen assassinationSuccededScreen,
            IAssassinationFailedScreen assassinationFailedScreen,
            IRevolutionScreen revolutionScreen,
            IRevolutionStartedScreen revolutionStartedScreen,
            IRevolutionAskForHelpDialog revolutionAskForHelpDialog,
            IRevolutionNoAlliesScreen revolutionNoAlliesScreen,
            IRevolutionCrushedDialog revolutionCrushedDialog,
            IRevolutionOverthrownScreen revolutionOverthrownScreen,
            IWarThreatScreen warThreatScreen,
            IWarLeftotoInvadesScreen leftotoInvadesScreen,
            IWarLostScreen warLostScreen,
            IWarWonScreen warWonScreen,
            IEscapeAttemptDialog escapeAttemptDialog,
            IHelicopterEscapeScreen helicopterEscapeScreen,
            IHelicopterWontStartScreen helicopterWontStartScreen,
            IEscapeToLeftotoScreen escapeToLeftotoScreen,
            IGuerillasCelebratingScreen guerillasCelebratingScreen,
            IGuerillasMissedScreen guerillasMissedScreen,
            IHelicopterEngineFailureScreen helicopterEngineFailureScreen,
            IWarExecutionScreen warExecutionScreen,
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
            this.loanApplicationScreen = loanApplicationScreen;
            this.loanApplicationResultScreen = loanApplicationResultScreen;
            this.transferToSwissBankAccountScreen = transferToSwissBankAccountScreen;
            this.assassinationScreen = assassinationScreen;
            this.assassinationSuccededScreen = assassinationSuccededScreen;
            this.assassinationFailedScreen = assassinationFailedScreen;
            this.revolutionScreen = revolutionScreen;
            this.revolutionStartedScreen = revolutionStartedScreen;
            this.revolutionAskForHelpDialog = revolutionAskForHelpDialog;
            this.revolutionNoAlliesScreen = revolutionNoAlliesScreen;
            this.revolutionCrushedDialog = revolutionCrushedDialog;
            this.revolutionOverthrownScreen = revolutionOverthrownScreen;
            this.warThreatScreen = warThreatScreen;
            this.leftotoInvadesScreen = leftotoInvadesScreen;
            this.warLostScreen = warLostScreen;
            this.warWonScreen = warWonScreen;
            this.escapeAttemptDialog = escapeAttemptDialog;
            this.helicopterEscapeScreen = helicopterEscapeScreen;
            this.helicopterWontStartScreen = helicopterWontStartScreen;
            this.escapeToLeftotoScreen = escapeToLeftotoScreen;
            this.guerillasCelebratingScreen = guerillasCelebratingScreen;
            this.guerillasMissedScreen = guerillasMissedScreen;
            this.helicopterEngineFailureScreen = helicopterEngineFailureScreen;
            this.warExecutionScreen = warExecutionScreen;
            this.endScreen = endScreen;
        }

        public void Initialise()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
        }

        public void DisplayIntroScreen() => introScreen.Show();

        public void DisplayWelcomeScreen(int highscore) => welcomeScreen.Show(highscore);

        public void DisplayTitleScreen() => titleScreen.Show();

        public void DisplayAccountScreen(Account account) => treasuryReportScreen.Show(account);

        public DialogResult DisplayPoliceReportRequestDialog(PoliceReportRequest policeReportRequest) => policeReportRequestScreen.Show(policeReportRequest);

        public void DisplayPoliceReportScreen(PoliceReport policeReport) => policeReportScreen.Show(policeReport);

        public void DisplayAudienceScreen(Core.Audience audience) => audienceScreen.Show(audience);

        public DialogResult DisplayAudienceDecisionDialog(Core.Audience audience) => audienceDecisionDialog.Show(audience);

        public DialogResult DisplayAdviceRequestDialog() => adviceRequestDialog.Show();

        public void DisplayAdviceScreen(Core.Audience audience) => adviceScreen.Show(audience);

        public void DisplayAdviceScreen(Decision decision) => adviceScreen.Show(decision);

        public void DisplayBankuptcyScreen() => bankruptcyScreen.Show();

        public void DisplayNewsScreen(string headline) => newsflashScreen.Show(headline);

        public void DisplayMonthScreen(int month) => monthScreen.Show(month);

        public DecisionType DisplayPresidentialDecisionMainDialog() => presidentialDecisionMainDialog.Show();

        public int DisplayPresidentialDecisionSubDialog(Decision[] decisions) => presidentialDecisionSubDialog.Show(decisions);

        public DialogResult DisplayPresidentialDecisionActionDialog(Decision decision) => presidentialDecisionActionDialog.Show(decision);

        public void DisplayLoanApplicationScreen() => loanApplicationScreen.Show();

        public void DisplayLoanApplicationResultScreen(LoanApplicationResult loanApplicationResult) => loanApplicationResultScreen.Show(loanApplicationResult);

        public void DisplayTransferToSwissBankAccount(SwissBankAccountTransfer swissBankAccountTransfer, Account account) => transferToSwissBankAccountScreen.Show(swissBankAccountTransfer, account);

        public void DisplayAssassinationAttempt(string groupName) => assassinationScreen.Show(groupName);

        public void DisplayAssassinationFailedScreen() => assassinationFailedScreen.Show();

        public void DisplayAssassinationSuccededScreen() => assassinationSuccededScreen.Show();

        public void DisplayRevolutionScreen() => revolutionScreen.Show();

        public void DisplayRevolutionStartedScreen() => revolutionStartedScreen.Show();

        public int DisplayRevolutionAskForHelpDialog(Dictionary<int, Group> possibleAllies) => revolutionAskForHelpDialog.Show(possibleAllies);

        public void DisplayRevolutionNoAlliesScreen() => revolutionNoAlliesScreen.Show();

        public DialogResult DisplayRevolutionCrushedDialog() => revolutionCrushedDialog.Show();

        public void DisplayRevolutionOverthrownScreen() => revolutionOverthrownScreen.Show();

        public void DisplayWarThreatScreen() => warThreatScreen.Show();

        public void DisplayLeftotoInvadesScreen(WarStats warStats) => leftotoInvadesScreen.Show(warStats);

        public void DisplayWarLostScreen() => warLostScreen.Show();

        public void DisplayWarWonScreen() => warWonScreen.Show();

        public DialogResult DisplayEscapeAttemptDialog() => escapeAttemptDialog.Show();

        public void DisplayHelicopterEscapeScreen() => helicopterEscapeScreen.Show();

        public void DisplayHelicopterWontStartScreen() => helicopterWontStartScreen.Show();

        public void DisplayEscapeToLeftotoScreen() => escapeToLeftotoScreen.Show();

        public void DisplayGuerillasMissedScreen() => guerillasMissedScreen.Show();

        public void DisplayGuerillasCelebratingScreen() => guerillasCelebratingScreen.Show();

        public void DisplayHelicopterEngineFailure() => helicopterEngineFailureScreen.Show();

        public void DisplayWarExecutionScreen() => warExecutionScreen.Show();

        public void DisplayEndScreen(Score score) => endScreen.Show(score);
    }
}
