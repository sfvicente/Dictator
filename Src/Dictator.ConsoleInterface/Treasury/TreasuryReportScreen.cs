using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.Treasury;

/// <summary>
///     Represents the screen that is displayed when a report of the treasury is required.
/// </summary>
public interface ITreasuryReportScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    /// <param name="account">The account information regarding the treasury, monthly costs and Swiss account.</param>
    public void Show(Account account);
}

/// <summary>
///     Represents the screen that is displayed when a report of the treasury is required.
/// </summary>
public class TreasuryReportScreen : BaseScreen, ITreasuryReportScreen
{
    private readonly IAccountControl accountControl;
    private readonly IPressAnyKeyControl pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TreasuryReportScreen"/> class from an <see cref="IAccountControl"/> and
    ///     a <see cref="IPressAnyKeyControl"/> components.
    /// </summary>
    /// <param name="accountControl">The control that displays the current balances of both the treasury and Swiss bank account, and
    ///     also displays the monthly costs.</param>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public TreasuryReportScreen(IConsoleService consoleService, IAccountControl accountControl, IPressAnyKeyControl pressAnyKeyControl)
        : base(consoleService)
    {
        this.accountControl = accountControl;
        this.pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    /// <param name="account">The account information regarding the treasury, monthly costs and Swiss account.</param>
    public void Show(Account account)
    {
        _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Green);
        _consoleService.Clear('$');
        _consoleService.WriteAt(2, 4, "                              ", ConsoleColor.Green, ConsoleColor.Black);
        _consoleService.WriteAt(2, 5, "                              ", ConsoleColor.Green, ConsoleColor.Black);
        _consoleService.WriteAt(2, 6, "                              ", ConsoleColor.Green, ConsoleColor.Black);
        _consoleService.WriteAt(8, 9, "TREASURY REPORT", ConsoleColor.White, ConsoleColor.Black);
        accountControl.Show(account);
        pressAnyKeyControl.Show();
    }
}
