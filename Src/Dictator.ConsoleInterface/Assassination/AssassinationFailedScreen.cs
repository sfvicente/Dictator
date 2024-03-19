using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Assassination;

/// <summary>
///     Represents the screen that is displayed when an assassination by one of the groups against the player
///     fails.
/// </summary>
public interface IAssassinationFailedScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed when an assassination by one of the groups against the player
///     fails.
/// </summary>
public class AssassinationFailedScreen : BaseScreen, IAssassinationFailedScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AssassinationFailedScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public AssassinationFailedScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl)
        : base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        _consoleService.Clear(ConsoleColor.Gray);
        _consoleService.WriteAt(1, 11, "         Attempt FAILED         ", ConsoleColor.Gray, ConsoleColor.Black);         
        _pressAnyKeyControl.Show();
    }
}
