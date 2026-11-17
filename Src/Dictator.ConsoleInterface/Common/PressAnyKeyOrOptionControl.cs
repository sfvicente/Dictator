using System;

namespace Dictator.ConsoleInterface.Common;

/// <summary>
///     Represents the control that is displayed with a panel when the user is required
///     to press a key or select an option.
/// </summary>
public interface IPressAnyKeyOrOptionControl
{
    /// <summary>
    ///     Displays the control.
    /// </summary>
    /// <returns>The console key that has was pressed after the control is displayed.</returns>
    public ConsoleKey Show();
}

/// <summary>
///     Represents the control that is displayed with a panel when the user is required
///     to press a key or select an option.
/// </summary>
public class PressAnyKeyOrOptionControl : IPressAnyKeyOrOptionControl
{
    private readonly IConsoleService _consoleService;
    private readonly IKeyPanel _keyPanel;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PressAnyKeyOrOptionControl"/> class from a <see cref="IConsoleService"/> and <see cref="IKeyPanel"/>
    ///     components.
    /// </summary>
    /// <param name="consoleService">The service used to interact with the console.</param>
    /// <param name="keyPanel">The panel that is displayed when the user is required to press a key.</param>
    public PressAnyKeyOrOptionControl(IConsoleService consoleService, IKeyPanel keyPanel)
    {
        _consoleService = consoleService;
        _keyPanel = keyPanel;
    }

    /// <summary>
    ///     Displays the control.
    /// </summary>
    /// <returns>The console key that has was pressed after the control is displayed.</returns>
    public ConsoleKey Show()
    {
        _keyPanel.Show();

        return _consoleService.ReadKey();
    }
}
