using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape;

public interface IEscapeToLeftotoScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed when the player attempts the escape
///     through the mountains to Leftoto.
/// </summary>
public class EscapeToLeftotoScreen : BaseScreen, IEscapeToLeftotoScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EscapeToLeftotoScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public EscapeToLeftotoScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.WriteAt(1, 10, "   You have to get through the  ");
        _consoleService.WriteAt(1, 12, "      MOUNTAINS to LEFTOTO      ");
        _pressAnyKeyControl.Show();
    }
}
