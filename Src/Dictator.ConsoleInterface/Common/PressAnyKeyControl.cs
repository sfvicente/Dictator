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
}

/// <summary>
///     Represents the control that is displayed with a panel when the user is required
///     to press a key.
/// </summary>
public class PressAnyKeyControl : IPressAnyKeyControl
{
    private readonly IKeyPanel _keyPanel;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PressAnyKeyControl"/> class from a <see cref="IKeyPanel"/>
    ///     component.
    /// </summary>
    /// <param name="keyPanel">The panel that is displayed when the user is required to press a key.</param>
    public PressAnyKeyControl(IKeyPanel keyPanel)
    {
        _keyPanel = keyPanel;
    }

    /// <summary>
    ///     Displays the control.
    /// </summary>
    public void Show()
    {
        _keyPanel.Show();
        Console.ReadKey(true);
    }
}
