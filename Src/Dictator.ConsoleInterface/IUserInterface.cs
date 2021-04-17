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
        public DialogResult DisplayPoliceReportRequestDialog();
        public void DisplayPoliceReportScreen();
        public void DisplayAudienceScreen(Core.Audience audience);
        public DialogResult DisplayAudienceDecisionDialog(Core.Audience audience);
        public DialogResult DisplayAdviceRequestDialog();
        public void DisplayAdviceScreen(Core.Audience audience);
        public void DisplayAdviceScreen(Decision decision);
        public void DisplayBankuptcyScreen();
        public void DisplayNewsScreen(string headline);
        public void DisplayMonthScreen();
        public DecisionType DisplayPresidentialDecisionMainDialog();
        public int DisplayPresidentialDecisionSubDialog(DecisionType decisionType);
        public DialogResult DisplayPresidentialDecisionActionDialog(Decision decision);
        public void DisplayLoanScreen();
        public void DisplayTransferToSwissBankAccount(SwissBankAccountTransfer swissBankAccountTransfer, Account account);
        public void DisplayAssassinationAttempt();
        public void DisplayAssassinationFailedScreen();
        public void DisplayAssassinationSuccededScreen();
        public void DisplayRevolutionScreen();
        public void DisplayWarThreatScreen();
        public void DisplayWarScreen();
        public void DisplayWarLostScreen();
        public void DisplayWarWonScreen();
        public DialogResult DisplayEscapeAttemptDialog();
        public void DisplayHelicopterEscapeScreen();
        public void DisplayHelicopterWontStartScreen();
        public void DisplayEscapeToLeftotoScreen();
        public void DisplayGuerillasMissedScreen();
        public void DisplayHelicopterEngineFailure();
        public void DisplayWarExecutionScreen();
        public void DisplayEndScreen();
    }
}