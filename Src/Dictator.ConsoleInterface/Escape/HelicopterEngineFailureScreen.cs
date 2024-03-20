using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape;

public interface IHelicopterEngineFailureScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed when the player attempts escape but the helicopter
///     engine fails.
/// </summary>
public class HelicopterEngineFailureScreen : BaseScreen, IHelicopterEngineFailureScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="HelicopterEngineFailureScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public HelicopterEngineFailureScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.WriteAt(1, 9, "    Helicopter ENGINE FAILURE   ");
        _consoleService.WriteAt(1, 11, "     YOU are judged to be an    ");
        _consoleService.WriteAt(1, 13, "   ENEMY of the PEOPLE and ...  ");
        _consoleService.WriteAt(1, 15, "       Summarily EXECUTED       ");
        _pressAnyKeyControl.Show();
    }
}
