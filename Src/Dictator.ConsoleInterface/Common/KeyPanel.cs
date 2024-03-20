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
        _consoleService.WriteAt(1, 22, "                                ", ConsoleColor.Blue, ConsoleColor.White);
        _consoleService.WriteAt(1, 23, "              KEY               ", ConsoleColor.Blue, ConsoleColor.White);
        _consoleService.WriteAt(1, 24, "                                ", ConsoleColor.Blue, ConsoleColor.White);
    }
}
