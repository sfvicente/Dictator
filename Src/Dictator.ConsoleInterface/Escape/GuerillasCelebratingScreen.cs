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
    public void Show()
    {
        _consoleService.Clear(ConsoleColor.Black, ConsoleColor.White);
        _consoleService.WriteAt(1, 12, " The GUERILLAS are ");
        _consoleService.SetFlash(true);
        _consoleService.Write("CELEBRATING", ConsoleColor.White);
        _consoleService.SetFlash(false);
        _pressAnyKeyControl.Show();
    }
}
