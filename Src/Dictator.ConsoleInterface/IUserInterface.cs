﻿namespace Dictator.ConsoleInterface
{
    public interface IUserInterface
    {
        void Initialise();
        public void DisplayIntroScreen();
        public void DisplayWelcomeScreen();
        public void DisplayTitleScreen();
        public void DisplayAccountScreen();
        public bool DisplayPoliceReportRequestDialog();
        public void DisplayPoliceReportScreen();
        public void DisplayAudienceScreen();
        DialogResult DisplayAudienceDecisionDialog();
        DialogResult DisplayAdviceRequestDialog();
        void DisplayAdviceScreen();
        public void DisplayBankuptcyScreen();
        public void DisplayNewsScreen();
        void DisplayMonthScreen();
        void DisplayMainDecisionDialog();
        void DisplayAssassinationAttempt();
        void DisplayAssassinationFailedScreen();
        DialogResult DisplayEscapeAttemptDialog();
        void DisplayEscapeByHelicopterScreen();
        void DisplayEscapeByHelicopterFailScreen();
        void DisplayEscapeToLeftotoScreen();
        void DisplayEndScreen();
    }
}