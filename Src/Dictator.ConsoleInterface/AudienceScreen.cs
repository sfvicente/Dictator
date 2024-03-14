using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface;

public interface IAudienceScreen
{
    public void Show(Audience audience);
}

public class AudienceScreen : IAudienceScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    public AudienceScreen(IPressAnyKeyControl pressAnyKeyControl)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    public void Show(Audience audience)
    {
        ConsoleEx.Clear();
        ConsoleEx.WriteEmptyLineAt(1, ConsoleColor.Green);
        ConsoleEx.WriteEmptyLineAt(2, ConsoleColor.Green);
        ConsoleEx.WriteEmptyLineAt(3, ConsoleColor.Green);
        ConsoleEx.WriteEmptyLineAt(4, ConsoleColor.Green);
        ConsoleEx.WriteAt(11, 4, "AN AUDIENCE", ConsoleColor.White, ConsoleColor.Black);
        ConsoleEx.WriteEmptyLineAt(5, ConsoleColor.Green);

        for (int row = 6; row < 22; row++)
        {
            ConsoleEx.WriteEmptyLineAt(row, ConsoleColor.DarkYellow);
        }

        ConsoleEx.WriteAt(1, 11, $" A request from {audience.Requester}", ConsoleColor.DarkYellow, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 15, " Will YOUR EXCELLENCY agree to  ", ConsoleColor.DarkYellow, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 17, $"{audience.Text}", ConsoleColor.Yellow, ConsoleColor.Black);
        _pressAnyKeyControl.Show();
    }
}