using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface;

public interface IEndScreen
{
    public void Show(Score score);
}

public class EndScreen : IEndScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    public EndScreen(IPressAnyKeyControl pressAnyKeyControl)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    public void Show(Score score)
    {
        // TODO: scores values need to be aligned to the right

        ConsoleEx.Clear(ConsoleColor.DarkYellow);
        ConsoleEx.WriteAt(5, 3, "Your RATING as PRESIDENT", ConsoleColor.White, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 6, $" Total POPULARITY - {score.TotalPopularity}  ", ConsoleColor.DarkYellow, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 8, $" MONTHS in OFFICE ({score.MonthsInOffice}x3) - {score.MonthsInOffice * 3}", ConsoleColor.DarkYellow, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 10, $" For staying alive - {score.PointsForStayingAlive}  ", ConsoleColor.DarkYellow, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 12, " For ", ConsoleColor.DarkYellow, ConsoleColor.Black);
        ConsoleEx.WriteAt(6, 12, $"MONEYGRABBING", ConsoleColor.Green, ConsoleColor.White);
        ConsoleEx.WriteAt(6, 13, $"(${score.MoneyGrabbed}.000,000 /00,000) - {score.PointsForMoneyGrabbing}", ConsoleColor.DarkYellow, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 16, $" Your TOTAL is ", ConsoleColor.DarkYellow, ConsoleColor.Black);
        ConsoleEx.Write($"{score.TotalScore}", ConsoleColor.Yellow, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 18, $"[ Highest Score so far is {score.HighestScore} ]", ConsoleColor.DarkYellow, ConsoleColor.Black);
        _pressAnyKeyControl.Show();
    }
}
