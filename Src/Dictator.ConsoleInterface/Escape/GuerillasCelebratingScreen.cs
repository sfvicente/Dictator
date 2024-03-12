using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape;

public interface IGuerillasCelebratingScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show();
}

public class GuerillasCelebratingScreen : IGuerillasCelebratingScreen
{
    private readonly IPressAnyKeyControl pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GuerillasCelebratingScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public GuerillasCelebratingScreen(IPressAnyKeyControl pressAnyKeyControl)
    {
        this.pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 11, " The GUERILLAS are CELEBRATING  ");
        pressAnyKeyControl.Show();
    }
}
