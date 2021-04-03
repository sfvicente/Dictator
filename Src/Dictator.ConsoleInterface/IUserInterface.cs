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
        public void DisplayAccountScreen();
        public bool DisplayPoliceReportRequestDialog();
        public void DisplayPoliceReportScreen();
        public void DisplayAudienceScreen();
        public DialogResult DisplayAudienceDecisionDialog();
        public DialogResult DisplayAdviceRequestDialog();
        public void DisplayAdviceScreen();
        public void DisplayBankuptcyScreen();
        public void DisplayNewsScreen();
        public void DisplayMonthScreen();
        public DecisionType DisplayPresidentialDecisionMainDialog();
        public int DisplayPresidentialDecisionSubDialog(DecisionType decisionType);
        public void DisplayPresidentialDecisionActionDialog();
        public void DisplayLoanScreen();
        public void DisplayAssassinationAttempt();
        public void DisplayAssassinationFailedScreen();
        public void DisplayAssassinationSuccededScreen();
        public void DisplayRevolutionScreen();
        public void DisplayWarScreen();
        public void DisplayWarLostScreen();
        public void DisplayWarWonScreen();
        public DialogResult DisplayEscapeAttemptDialog();
        public void DisplayEscapeByHelicopterScreen();
        public void DisplayEscapeByHelicopterFailScreen();
        public void DisplayEscapeToLeftotoScreen();
        public void DisplayEndScreen();
    }
}