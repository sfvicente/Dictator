using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.War;

public interface IWarWonScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed when a player wins the war against Leftoto.
/// </summary>
public class WarWonScreen : BaseScreen, IWarWonScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="WarWonScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public WarWonScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        _consoleService.Clear(ConsoleColor.Gray);
        _consoleService.WriteAt(1, 11, "        LEFTOTANS ROUTED        ");
        _pressAnyKeyControl.Show();
    }
}
