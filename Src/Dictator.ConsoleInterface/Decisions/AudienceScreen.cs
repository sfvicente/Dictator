using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.Decisions;

public interface IAudienceScreen
{
    public void Show(Audience audience);
}

public class AudienceScreen : BaseScreen, IAudienceScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    public AudienceScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl)
        : base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    public void Show(Audience audience)
    {
        _consoleService.Clear();
        _consoleService.WriteEmptyLineAt(1, ConsoleColor.Green);
        _consoleService.WriteEmptyLineAt(2, ConsoleColor.Green);
        _consoleService.WriteEmptyLineAt(3, ConsoleColor.Green);
        _consoleService.WriteEmptyLineAt(4, ConsoleColor.Green);
        _consoleService.WriteAt(11, 4, "AN AUDIENCE", ConsoleColor.White, ConsoleColor.Black);
        _consoleService.WriteEmptyLineAt(5, ConsoleColor.Green);

        for (int row = 6; row < 22; row++)
        {
            _consoleService.WriteEmptyLineAt(row, ConsoleColor.DarkYellow);
        }

        _consoleService.WriteAt(1, 11, $" A request from {audience.Requester}", ConsoleColor.DarkYellow, ConsoleColor.Black);
        _consoleService.WriteAt(1, 15, " Will YOUR EXCELLENCY agree to  ", ConsoleColor.DarkYellow, ConsoleColor.Black);
        _consoleService.WriteAt(1, 17, $"{audience.Text}", ConsoleColor.Yellow, ConsoleColor.Black);
        _pressAnyKeyControl.Show();
    }
}