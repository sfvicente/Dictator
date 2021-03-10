﻿namespace Dictator.ConsoleInterface
{
    public interface IUserInterface
    {
        public void DisplayIntroScreen();
        public void DisplayWelcomeScreen();
        public void DisplayTitleScreen();
        public void DisplayAccountScreen();
        public bool DisplayPoliceReportRequestDialog();
        public void DisplayPoliceReportScreen();
        public void DisplayAudienceScreen();
        void DisplayAdviceScreen();
        public void DisplayBankuptcyScreen();
        public void DisplayNewsScreen();
        void DisplayMonthScreen();
        void DisplayMainDecisionDialog();
        void DisplayAssassinationAttempt();
        void DisplayEscapeByHelicopterScreen();
        void DisplayEscapeByHelicopterFailScreen();
        void DisplayEscapeToLeftotoScreen();
        void DisplayEndScreen();
    }
}