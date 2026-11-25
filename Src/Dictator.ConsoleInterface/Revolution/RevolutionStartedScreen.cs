using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.Revolution;

/// <summary>
///     Represents the screen that is displayed when a revolution has started.
/// </summary>
public interface IRevolutionStartedScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show();

    /// <summary>
    ///     Displays the pre-revolution strength comparison and revolution started screen.
    /// </summary>
    public void Show(RevolutionState revolutionState);
}

/// <summary>
///     Represents the screen that is displayed when a revolution has started.
/// </summary>
public class RevolutionStartedScreen : BaseScreen, IRevolutionStartedScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RevolutionStartedScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public RevolutionStartedScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the pre-revolution strength comparison and the revolution started screen.
    /// </summary>
    public void Show(RevolutionState revolutionState)
    {
        _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.WriteAt(1, 9, $"YOUR STRENGTH is {revolutionState.PlayerStrength}");

        if (!string.IsNullOrEmpty(revolutionState.AllyName))
        {
            _consoleService.WriteAt(1, 11, $"{revolutionState.AllyName} strength is {revolutionState.AllyStrength}");
        }

        _consoleService.WriteAt(1, 13, $"The REVOLUTIONARY'S is {revolutionState.RevolutionaryStrength}");
        _consoleService.SetFlash(true);
        _consoleService.WriteAt(1, 17, "   The REVOLUTION has STARTED   ");
        _consoleService.SetFlash(false);
        _pressAnyKeyControl.Show();
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.SetFlash(true);
        _consoleService.WriteAt(1, 12, "   The REVOLUTION has STARTED   ");
        _consoleService.SetFlash(false);
        _pressAnyKeyControl.Show();
    }
}
