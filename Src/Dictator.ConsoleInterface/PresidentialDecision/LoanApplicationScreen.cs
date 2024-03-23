using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterface.PresidentialDecision;

/// <summary>
///     Represents the screen that is displayed when the player makes an application 
///     for monetary foreign aid.
/// </summary>
public interface ILoanApplicationScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed when the player makes an application 
///     for monetary foreign aid.
/// </summary>
public class LoanApplicationScreen : BaseScreen, ILoanApplicationScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LoanApplicationScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public LoanApplicationScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        _consoleService.Clear();
        _consoleService.WriteAt(1, 3, "  APPLICATION for FOREIGN AID   ");
        _consoleService.WriteAt(1, 12, "              WAIT              ");
        _pressAnyKeyControl.Show();
    }
}
