using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Assassination;

/// <summary>
///     Represents the screen that is displayed when an assassination by one of the groups against the player
///     succeeds.
/// </summary>
public interface IAssassinationSucceededScreen
{
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed when an assassination by one of the groups against the player
///     succeeds.
/// </summary>
public class AssassinationSucceededScreen : BaseScreen, IAssassinationSucceededScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AssassinationSucceededScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public AssassinationSucceededScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl)
        : base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    public void Show()
    {
        _consoleService.Clear(ConsoleColor.Gray);
        _consoleService.WriteAt(1, 11, "          You're DEAD !         ", ConsoleColor.Gray, ConsoleColor.Black);
        _pressAnyKeyControl.Show();
    }
}
