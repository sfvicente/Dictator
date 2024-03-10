using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Linq;

namespace Dictator.ConsoleInterface;

public interface IPoliceReportScreen
{
    public void Show(PoliceReport policeReport);
}

/// <summary>
///     Represents the screen that is displayed when the player is shown the police report.
/// </summary>
public class PoliceReportScreen : IPoliceReportScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PoliceReportScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public PoliceReportScreen(IPressAnyKeyControl pressAnyKeyControl)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    public void Show(PoliceReport policeReport)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        ConsoleEx.Clear();
        DisplayHeaders(policeReport.Month);
        DisplayGroupStats(policeReport.Groups.ToArray());
        DisplayGovernmentStats(policeReport.PlayerStrength, policeReport.MonthlyRevolutionStrength);
        _pressAnyKeyControl.Show();
    }

    private void DisplayHeaders(int month)
    {
        ConsoleEx.WriteAt(1, 1, $"MONTH {month}                       ");
        ConsoleEx.WriteAt(1, 4, "      ", ConsoleColor.Blue, ConsoleColor.Black);
        ConsoleEx.WriteAt(7, 4, "SECRET POLICE REPORT", ConsoleColor.White, ConsoleColor.Black);
        ConsoleEx.WriteAt(27, 4, "      ", ConsoleColor.Blue, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 7, " POPULARITY          STRENGTHS ");
    }

    private void DisplayGroupStats(Group[] groups)
    {
        for (int i = 0; i < groups.Length; i++)
        {
            int currentPopularity = groups[i].Popularity;
            int popularityStartIndex = 11 - currentPopularity;

            ConsoleEx.SetCursorPosition(popularityStartIndex, 9 + i);

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            for (int p = currentPopularity; p > 0; p--)
            {
                Console.Write(p);
            }

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(i + 1);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            ConsoleEx.WriteAt(12, 9 + i, $"{groups[i].DisplayName}");

            DisplayGroupStatus(groups[i]);

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            for (int s = 1; s <= groups[i].Strength; s++)
            {
                Console.Write(s);
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    private void DisplayGroupStatus(Group group)
    {
        if (group.Status == GroupStatus.Assassination)
        {
            ConsoleEx.Write("A", ConsoleColor.White, ConsoleColor.Black);
        }
        else if (group.Status == GroupStatus.Revolution)
        {
            ConsoleEx.Write("R", ConsoleColor.White, ConsoleColor.Black);
        }
        else
        {
            ConsoleEx.Write(" ", ConsoleColor.Black, ConsoleColor.Black);
        }
    }

    private void DisplayGovernmentStats(int playerStrength, int monthlyRevolutionStrength)
    {
        ConsoleEx.WriteAt(1, 18, $"  Your STRENGTH is {playerStrength}           ");
        ConsoleEx.WriteAt(1, 20, $"  STRENGTH for REVOLUTION is {monthlyRevolutionStrength} ");
    }
}
