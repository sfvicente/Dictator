using System;

namespace Dictator.ConsoleInterface.Common;

/// <summary>
///     Represents the panel that is displayed when the user is required to press a key.
/// </summary>
public interface IKeyPanel
{
    /// <summary>
    ///     Displays the panel.
    /// </summary>
    public void Show();

    /// <summary>
    ///     Displays the panel with a custom background color.
    /// </summary>
    public void Show(ConsoleColor backgroundColor);
}

/// <summary>
///     Represents the panel that is displayed when the user is required to press a key.
/// </summary>
public class KeyPanel : BaseScreen, IKeyPanel
{
    public KeyPanel(IConsoleService console)
        : base(console)
    {

    }

    /// <summary>
    ///     Displays the panel.
    /// </summary>
    public void Show()
    {
        Show(ConsoleColor.DarkRed);
    }

    /// <summary>
    ///     Displays the panel with a custom background color.
    /// </summary>
    public void Show(ConsoleColor backgroundColor)
    {
        _consoleService.WriteAt(1, 22, "                                ", backgroundColor, ConsoleColor.White);
        _consoleService.WriteAt(1, 23, "              KEY               ", backgroundColor, ConsoleColor.White);
        _consoleService.WriteAt(1, 24, "                                ", backgroundColor, ConsoleColor.White);
    }
}
