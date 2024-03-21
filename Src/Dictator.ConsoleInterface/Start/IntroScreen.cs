using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Start;

/// <summary>
///     Represents the screen that is displayed when a game starts.
/// </summary>
public interface IIntroScreen
{
    public void Show();
}

/// <summary>
///     Represents the screen that is displayed when a game starts.
/// </summary>
public class IntroScreen : BaseScreen, IIntroScreen
{
    private readonly IPressAnyKeyControl pressAnyKeyControl;

    public IntroScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl)
        : base(consoleService)
    {
        this.pressAnyKeyControl = pressAnyKeyControl;
    }

    public void Show()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        _consoleService.Clear();
        _consoleService.WriteAt(3, 5, "DICTATOR", ConsoleColor.White, ConsoleColor.Black);
        _consoleService.WriteAt(1, 9, "  Devised and Written by        ", ConsoleColor.Gray);
        _consoleService.WriteAt(1, 11, "  Don PRIESTLEY                 ", ConsoleColor.Gray);
        _consoleService.WriteAt(1, 15, "  Copyright  ", ConsoleColor.White);
        _consoleService.Write("D", ConsoleColor.White, ConsoleColor.Black);
        _consoleService.Write("k", ConsoleColor.Yellow, ConsoleColor.Black);
        _consoleService.Write("T", ConsoleColor.White, ConsoleColor.Black);
        _consoleService.Write("R", ConsoleColor.Green, ConsoleColor.Black);
        _consoleService.Write("O", ConsoleColor.Cyan, ConsoleColor.Black);
        _consoleService.Write("N", ConsoleColor.Green, ConsoleColor.Black);
        _consoleService.Write("I", ConsoleColor.Cyan, ConsoleColor.Black);
        _consoleService.Write("C", ConsoleColor.Green, ConsoleColor.Black);
        _consoleService.Write("S", ConsoleColor.Cyan, ConsoleColor.Black);
        _consoleService.Write("  1983", ConsoleColor.White);
        _consoleService.WriteAt(1, 18, "  Rewritten in C# by ", ConsoleColor.Gray);
        _consoleService.WriteAt(1, 20, "  Sergio Vicente 2021  ", ConsoleColor.Gray);
        pressAnyKeyControl.Show();
    }
}
