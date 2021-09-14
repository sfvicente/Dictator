using Dictator.Common;
using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    /// <summary>
    ///     Represents the screen that is displayed when the player makes an application 
    ///     for monetary foreign aid.
    /// </summary>
    public class LoanApplicationScreen : ILoanApplicationScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public LoanApplicationScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 3, "  APPLICATION for FOREIGN AID   ");
            ConsoleEx.WriteAt(1, 12, "              WAIT              ");
            pressAnyKeyControl.Show();
        }
    }
}
