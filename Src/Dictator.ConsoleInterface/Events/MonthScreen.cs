using System;

namespace Dictator.ConsoleInterface.Events;

/// <summary>
///     Represents the screen that is displayed when a new month starts, which
///     marks a new game turn.
/// </summary>
public interface IMonthScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    /// <param name="month">The number of the month to display on the screen.</param>
    public void Show(int month);
}

/// <summary>
///     Represents the screen that is displayed when a new month starts, which
///     marks a new game turn.
/// </summary>
public class MonthScreen : BaseScreen, IMonthScreen
{
    public MonthScreen(IConsoleService console)
        : base(console)
    {

    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    /// <param name="month">The number of the month to display on the screen.</param>
    public void Show(int month)
    {
        _consoleService.Clear(ConsoleColor.Yellow);
        _consoleService.WriteAt(8, 10, "MONTH ", ConsoleColor.Cyan, ConsoleColor.Black);
        _consoleService.WriteAt(14, 10, $"{month}", ConsoleColor.White, ConsoleColor.Black);
        Console.ReadKey(true);
    }
}
