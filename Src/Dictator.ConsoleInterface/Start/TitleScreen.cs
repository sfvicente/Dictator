using System;

namespace Dictator.ConsoleInterface.Start;

/// <summary>
///     Represents the screen that is displayed with the title of the game.
/// </summary>
public interface ITitleScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed with the title of the game.
/// </summary>
public class TitleScreen : BaseScreen, ITitleScreen
{
    public TitleScreen(IConsoleService consoleService)
        : base(consoleService)
    {
        
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    public void Show()
    {
        _consoleService.SetBorderColor(ConsoleColor.Black);
        _consoleService.Clear();
        PrintBackground();
        PrintContent();
        _consoleService.WaitForKeyPressed();
    }

    private void PrintBackground()
    {
        _consoleService.SetFlash(true);
        for (int i = 1; i < 25; i++)
        {
            _consoleService.WriteAt(1, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.DarkCyan);
            _consoleService.WriteAt(9, i, "DICTATOR", ConsoleColor.DarkCyan, ConsoleColor.Black);
            _consoleService.WriteAt(17, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.DarkCyan);
            _consoleService.WriteAt(25, i, "DICTATOR", ConsoleColor.DarkCyan, ConsoleColor.Black);
        }
        _consoleService.SetFlash(false);
    }

    private void PrintContent()
    {
        _consoleService.WriteAt(1, 4, "Press any key to become DICTATOR", ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.WriteAt(13, 6, " of the ", ConsoleColor.Gray, ConsoleColor.Black);
        PrintFlagLine(9);
        PrintFlagLine(10);
        _consoleService.WriteAt(8, 11, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
        _consoleService.WriteAt(12, 11, " ", ConsoleColor.DarkBlue, ConsoleColor.DarkYellow);
        _consoleService.WriteAt(13, 11, "RITIMBAN", ConsoleColor.DarkGreen, ConsoleColor.Black);
        _consoleService.WriteAt(21, 11, " ", ConsoleColor.DarkBlue, ConsoleColor.DarkYellow);
        _consoleService.WriteAt(22, 11, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
        PrintFlagLine(12);
        _consoleService.WriteAt(8, 13, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
        _consoleService.WriteAt(12, 13, "  ******  ", ConsoleColor.DarkBlue, ConsoleColor.DarkYellow);
        _consoleService.WriteAt(22, 13, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
        _consoleService.WriteAt(8, 14, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
        _consoleService.WriteAt(12, 14, "    **    ", ConsoleColor.DarkBlue, ConsoleColor.DarkYellow);
        _consoleService.WriteAt(22, 14, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
        _consoleService.WriteAt(8, 15, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
        _consoleService.WriteAt(12, 15, "  ******  ", ConsoleColor.DarkBlue, ConsoleColor.DarkYellow);
        _consoleService.WriteAt(22, 15, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
        PrintFlagLine(16);
        _consoleService.WriteAt(8, 17, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
        _consoleService.WriteAt(12, 17, " ", ConsoleColor.DarkBlue, ConsoleColor.DarkYellow);
        _consoleService.WriteAt(13, 17, "REPUBLIC", ConsoleColor.DarkGreen, ConsoleColor.Black);
        _consoleService.WriteAt(21, 17, " ", ConsoleColor.DarkBlue, ConsoleColor.DarkYellow);
        _consoleService.WriteAt(22, 17, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
        PrintFlagLine(18);
        PrintFlagLine(19);
        PrintFlagLine(20);
    }

    private void PrintFlagLine(int row)
    {
        _consoleService.WriteAt(8, row, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
        _consoleService.WriteAt(12, row, "          ", ConsoleColor.DarkBlue, ConsoleColor.DarkYellow);
        _consoleService.WriteAt(22, row, "\u008C\u008C\u008C\u008C", ConsoleColor.DarkGreen, ConsoleColor.DarkRed);
    }
}
