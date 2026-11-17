using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;
using System.Linq;

namespace Dictator.ConsoleInterface.Police;

public interface IPoliceReportScreen
{
    public void Show(PoliceReport policeReport);
}

/// <summary>
///     Represents the screen that is displayed when the player is shown the police report.
/// </summary>
public class PoliceReportScreen : BaseScreen, IPoliceReportScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PoliceReportScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public PoliceReportScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl)
        : base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    public void Show(PoliceReport policeReport)
    {
        _consoleService.SetBorderColor(ConsoleColor.Black);
        _consoleService.SetBackgroundColor(ConsoleColor.Black);
        _consoleService.SetForegroundColor(ConsoleColor.White);
        _consoleService.Clear();
        DisplayHeaders(policeReport.Month);
        DisplayGroupStats(policeReport.Groups.ToArray());
        DisplayGovernmentStats(policeReport.PlayerStrength, policeReport.MonthlyRevolutionStrength);
        _pressAnyKeyControl.Show(ConsoleColor.DarkBlue);
    }

    private void DisplayHeaders(int month)
    {
        _consoleService.WriteAt(1, 1, $"MONTH {month}                       ");
        _consoleService.WriteAt(1, 4, "        ", ConsoleColor.DarkBlue, ConsoleColor.Black);
        _consoleService.WriteAt(9, 4, " POLICE  REPORT ", ConsoleColor.White, ConsoleColor.Black);
        _consoleService.WriteAt(25, 4, "        ", ConsoleColor.DarkBlue, ConsoleColor.Black);
        _consoleService.WriteAt(14, 6, "\u009B\u009C\u009D", ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.WriteAt(1, 7, " POPULARITY          STRENGTHS ");
        _consoleService.WriteAt(14, 7, "\u009E\u009F\u00A0", ConsoleColor.Gray, ConsoleColor.Black);
    }

    private void DisplayGroupStats(Group[] groups)
    {
        for (int i = 0; i < groups.Length; i++)
        {
            int currentPopularity = groups[i].Popularity;
            int popularityStartIndex = 11 - currentPopularity;

            _consoleService.SetCursorPosition(popularityStartIndex, 9 + i);

            for (int p = currentPopularity; p > 0; p--)
            {
                _consoleService.Write(p.ToString(), ConsoleColor.Green, ConsoleColor.White);
            }

            _consoleService.Write((i + 1).ToString(), ConsoleColor.White, ConsoleColor.Black);
            _consoleService.WriteAt(12, 9 + i, $"{groups[i].DisplayName}", ConsoleColor.Yellow, ConsoleColor.Black);

            DisplayGroupStatus(groups[i]);

            for (int s = 1; s <= groups[i].Strength; s++)
            {
                _consoleService.Write(s.ToString(), ConsoleColor.Red, ConsoleColor.White);
            }

            _consoleService.SetBackgroundColor(ConsoleColor.Black);
            _consoleService.SetForegroundColor(ConsoleColor.White);
        }
    }

    private void DisplayGroupStatus(Group group)
    {
        if (group.Status == GroupStatus.Assassination)
        {
            _consoleService.Write("A", ConsoleColor.White, ConsoleColor.Black);
        }
        else if (group.Status == GroupStatus.Revolution)
        {
            _consoleService.Write("R", ConsoleColor.White, ConsoleColor.Black);
        }
        else
        {
            _consoleService.Write(" ", ConsoleColor.Black, ConsoleColor.Black);
        }
    }

    private void DisplayGovernmentStats(int playerStrength, int monthlyRevolutionStrength)
    {
        _consoleService.WriteAt(1, 18, $"  Your STRENGTH is {playerStrength}           ");
        _consoleService.WriteAt(1, 20, $"  STRENGTH for REVOLUTION is {monthlyRevolutionStrength} ");
    }
}
