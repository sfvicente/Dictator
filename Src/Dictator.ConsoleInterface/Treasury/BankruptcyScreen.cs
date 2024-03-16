using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterface.Treasury
{
    /// <summary>
    ///     Represents the screen that is displayed when the treasury is bankrupt, leading to a drop in
    ///     the player's popularity.
    /// </summary>
    public class BankruptcyScreen: BaseScreen, IBankruptcyScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BankruptcyScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public BankruptcyScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl)
            : base(consoleService)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public override void Show()
        {
            _consoleService.Clear();
            _consoleService.WriteAt(1, 5, "   The TREASURY is BANKRUPT    ");
            _consoleService.WriteAt(1, 9, "Your POPULARITY with the ARMY &");
            _consoleService.WriteAt(1, 11, " the SECRET POLICE will DROP ! ");
            _consoleService.WriteAt(1, 13, "    POLICE STRENGTH will drop  ");
            _consoleService.WriteAt(1, 15, "and YOUR own STRENGTH will DROP");
            pressAnyKeyControl.Show();
        }
    }
}
