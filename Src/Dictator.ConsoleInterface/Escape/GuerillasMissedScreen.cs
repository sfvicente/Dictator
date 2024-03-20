using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape;

public interface IGuerillasMissedScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed when the player escapes through the mountains to Leftoto
///     and isn't caught by the guerillas.
/// </summary>
public class GuerillasMissedScreen : BaseScreen, IGuerillasMissedScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GuerillasMissedScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public GuerillasMissedScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.WriteAt(1, 11, " The GUERILLAS didn't catch you ");
        _pressAnyKeyControl.Show();
    }
}
