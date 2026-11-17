using System;

namespace Dictator.ConsoleInterface.Common;

/// <summary>
///     Represents the control that is displayed with a panel when the user is required
///     to press a key.
/// </summary>
public interface IPressAnyKeyControl
{
    /// <summary>
    ///     Displays the control.
    /// </summary>
    public void Show();

    /// <summary>
    ///     Displays the control with a custom panel background color.
    /// </summary>
    public void Show(ConsoleColor backgroundColor);
}

/// <summary>
///     Represents the control that is displayed with a panel when the user is required
///     to press a key.
/// </summary>
public class PressAnyKeyControl : IPressAnyKeyControl
{
    private readonly IConsoleService _consoleService;
    private readonly IKeyPanel _keyPanel;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PressAnyKeyControl"/> class from a <see cref="IConsoleService"/> and <see cref="IKeyPanel"/>
    ///     components.
    /// </summary>
    /// <param name="consoleService">The service used to interact with the console.</param>
    /// <param name="keyPanel">The panel that is displayed when the user is required to press a key.</param>
    public PressAnyKeyControl(IConsoleService consoleService, IKeyPanel keyPanel)
    {
        _consoleService = consoleService;
        _keyPanel = keyPanel;
    }

    /// <summary>
    ///     Displays the control.
    /// </summary>
    public void Show()
    {
        _keyPanel.Show();
        _consoleService.WaitForKeyPressed();
    }

    /// <summary>
    ///     Displays the control with a custom panel background color.
    /// </summary>
    public void Show(ConsoleColor backgroundColor)
    {
        _keyPanel.Show(backgroundColor);
        _consoleService.WaitForKeyPressed();
    }
}
