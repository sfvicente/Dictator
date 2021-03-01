namespace Dictator.ConsoleInterface
{
    public interface IUserInterface
    {
        public void DisplayIntroScreen();
        public void DisplayWelcomeScreen();
        public void DisplayTitleScreen();
        public void DisplayAccountScreen();
        public void DisplayPoliceReportRequestScreen();
        public void DisplayPoliceReportScreen();
        public void DisplayRequestScreen();
        public void DisplayBankuptcyScreen();
        public void DisplayNewsScreen();
    }
}