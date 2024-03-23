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
    private readonly IPressAnyKeyControl pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RevolutionOverthrownScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public RevolutionOverthrownScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
    {
        this.pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.WriteAt(1, 10, "    You have been OVERTHROWN    ");
        _consoleService.WriteAt(1, 12, "         and LIQUIDATED         ");
        pressAnyKeyControl.Show();
    }
}
