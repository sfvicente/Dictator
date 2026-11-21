using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution;

/// <summary>
///     Represents the screen that is displayed when the player is overthrown in the context of a revolution.
/// </summary>
public interface IRevolutionOverthrownScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed when the player is overthrown in the context of a revolution.
/// </summary>
public class RevolutionOverthrownScreen : BaseScreen, IRevolutionOverthrownScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RevolutionOverthrownScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public RevolutionOverthrownScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        _consoleService.SetBorderColor(ConsoleColor.Black);
        _consoleService.Clear(ConsoleColor.Black, ConsoleColor.White);
        _consoleService.WriteAt(1, 10, "    You have been OVERTHROWN    ");
        _consoleService.WriteAt(1, 12, "         and ");
        _consoleService.SetFlash(true);
        _consoleService.Write("LIQUIDATED", ConsoleColor.White);
        _consoleService.SetFlash(false);
        _pressAnyKeyControl.Show();
    }
}
