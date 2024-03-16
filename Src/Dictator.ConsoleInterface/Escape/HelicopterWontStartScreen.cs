using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape;

public interface IHelicopterWontStartScreen
{
    /// <summary>
    ///     Displays the screen
    /// </summary>
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed when the player attempts escape using the helicopter
///     but it won't start.
/// </summary>
public class HelicopterWontStartScreen : BaseScreen, IHelicopterWontStartScreen
{
    private readonly IPressAnyKeyControl pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="HelicopterWontStartScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public HelicopterWontStartScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
    {
        this.pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen
    /// </summary>
    public void Show()
    {
        _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.WriteAt(1, 12, "  The HELICOPTER won't START !  ");
        pressAnyKeyControl.Show();
    }
}
