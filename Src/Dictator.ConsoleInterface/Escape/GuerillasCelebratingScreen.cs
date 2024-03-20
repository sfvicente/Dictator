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

public class GuerillasCelebratingScreen : BaseScreen, IGuerillasCelebratingScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GuerillasCelebratingScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public GuerillasCelebratingScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl)
        : base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public override void Show()
    {
        _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.WriteAt(1, 11, " The GUERILLAS are CELEBRATING  ");
        _pressAnyKeyControl.Show();
    }
}
