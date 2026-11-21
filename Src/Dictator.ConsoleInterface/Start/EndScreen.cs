using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.Start;

public interface IEndScreen
{
    public void Show(Score score);
}

public class EndScreen : BaseScreen, IEndScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    public EndScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl)
        : base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    public void Show(Score score)
    {
        _consoleService.SetBorderColor(ConsoleColor.DarkCyan);
        _consoleService.Clear(ConsoleColor.DarkYellow);
        _consoleService.SetFlash(true);
        _consoleService.WriteAt(5, 4, "Your RATING as PRESIDENT", ConsoleColor.White, ConsoleColor.Black);
        _consoleService.SetFlash(false);

        _consoleService.WriteAt(1, 6, " Total POPULARITY - ", ConsoleColor.DarkYellow, ConsoleColor.Black);
        _consoleService.WriteAt(29, 6, $"{score.TotalPopularity}", ConsoleColor.DarkYellow, ConsoleColor.Black);

        _consoleService.WriteAt(1, 8, $" MONTHS in OFFICE ({score.MonthsInOffice}x3) - ", ConsoleColor.DarkYellow, ConsoleColor.Black);
        _consoleService.WriteAt(29, 8, $"{score.PointsForMonthsInOffice}", ConsoleColor.DarkYellow, ConsoleColor.Black);

        if (score.PointsForStayingAlive > 0)
        {
            _consoleService.WriteAt(1, 10, " For staying ALIVE - ", ConsoleColor.DarkYellow, ConsoleColor.Black);
            _consoleService.WriteAt(29, 10, $"{score.PointsForStayingAlive}", ConsoleColor.DarkYellow, ConsoleColor.Black);

            _consoleService.WriteAt(1, 12, " For ", ConsoleColor.DarkYellow, ConsoleColor.Black);
            _consoleService.WriteAt(6, 12, "MONEYGRABBING", ConsoleColor.DarkGreen, ConsoleColor.White);
            _consoleService.WriteAt(6, 13, $"(${score.MoneyGrabbed},000 /10,000) -", ConsoleColor.DarkYellow, ConsoleColor.Black);
            _consoleService.WriteAt(29, 13, $"{score.PointsForMoneyGrabbing}", ConsoleColor.White, ConsoleColor.Black);
        }

        _consoleService.WriteAt(1, 16, " Your TOTAL is ", ConsoleColor.DarkYellow, ConsoleColor.Black);
        _consoleService.WriteAt(29, 16, $"{score.TotalScore}", ConsoleColor.Yellow, ConsoleColor.Black);

        int displayedHighest = Math.Max(score.HighestScore, score.TotalScore);
        _consoleService.WriteAt(1, 18, $"[ Highest Score so far is {displayedHighest} ]", ConsoleColor.DarkYellow, ConsoleColor.Black);
        _pressAnyKeyControl.Show();
    }
}
