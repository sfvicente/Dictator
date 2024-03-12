using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape;

public interface IHelicopterEscapeScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed when the player successfully escapes using the helicopter.
/// </summary>
public class HelicopterEscapeScreen : IHelicopterEscapeScreen
{
    private readonly IPressAnyKeyControl pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="HelicopterEscapeScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public HelicopterEscapeScreen(IPressAnyKeyControl pressAnyKeyControl)
    {
        this.pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 12, "   You ESCAPE by HELICOPTER !   ");
        pressAnyKeyControl.Show();
    }
}
