using Dictator.ConsoleInterface.Common;
using Dictator.Core;

namespace Dictator.ConsoleInterface
{
    public interface IUserInterface
    {
        public void Initialise();
        public void DisplayIntroScreen();
        public void DisplayWelcomeScreen();
        public void DisplayTitleScreen();
        public void DisplayAccountScreen(Account account);
        public DialogResult DisplayPoliceReportRequestDialog(PoliceReportRequest policeReportRequest);
        public void DisplayPoliceReportScreen();
        public void DisplayAudienceScreen(Core.Audience audience);
        public DialogResult DisplayAudienceDecisionDialog(Core.Audience audience);
        public DialogResult DisplayAdviceRequestDialog();
        public void DisplayAdviceScreen(Core.Audience audience);
        public void DisplayAdviceScreen(Decision decision);
        public void DisplayBankuptcyScreen();
        public void DisplayNewsScreen(string headline);
        public void DisplayMonthScreen(int month);
        public DecisionType DisplayPresidentialDecisionMainDialog();
        public int DisplayPresidentialDecisionSubDialog(DecisionType decisionType);
        public DialogResult DisplayPresidentialDecisionActionDialog(Decision decision);
        public void DisplayLoanApplicationScreen();
        public void DisplayLoanApplicationResultScreen(LoanApplicationResult loanApplicationResult);
        public void DisplayTransferToSwissBankAccount(SwissBankAccountTransfer swissBankAccountTransfer, Account account);
        public void DisplayAssassinationAttempt();
        public void DisplayAssassinationFailedScreen();
        public void DisplayAssassinationSuccededScreen();
        public void DisplayRevolutionScreen();
        public void DisplayWarThreatScreen();
        public void DisplayWarScreen();
        public void DisplayLeftotoInvadesScreen(WarStats warStats);
        public void DisplayWarLostScreen();
        public void DisplayWarWonScreen();
        public DialogResult DisplayEscapeAttemptDialog();
        public void DisplayHelicopterEscapeScreen();
        public void DisplayHelicopterWontStartScreen();
        public void DisplayEscapeToLeftotoScreen();
        public void DisplayGuerillasCelebratingScreen();
        public void DisplayGuerillasMissedScreen();
        public void DisplayHelicopterEngineFailure();
        public void DisplayWarExecutionScreen();
        public void DisplayEndScreen(Score score);
    }
}