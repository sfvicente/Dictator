using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using Dictator.Core.Services;
using System;

namespace Dictator.ConsoleInterface;

public interface IAdviceScreen
{
    public void Show(GameAction gameAction);
}

public class AdviceScreen : IAdviceScreen
{
    private readonly IGroupService _groupService;
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    public AdviceScreen(IGroupService groupService, IPressAnyKeyControl pressAnyKeyControl)
    {
        _groupService = groupService;
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    /// <param name="gameAction">The game action to be displayed on the screen.</param>
    public void Show(GameAction gameAction)
    {
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        ConsoleEx.Clear();
        ConsoleEx.WriteAt(1, 2, $"{gameAction.Text}", ConsoleColor.Black, ConsoleColor.DarkYellow);
        ConsoleEx.WriteAt(1, 4, "Your POPULARITY with", ConsoleColor.Yellow, ConsoleColor.Black);
        ConsoleEx.Write(" ....", ConsoleColor.Black);
        DisplayPopularityChanges(gameAction.GroupPopularityChanges);
        ConsoleEx.WriteAt(1, Console.CursorTop + 3, "The STRENGTH of", ConsoleColor.Yellow, ConsoleColor.Black);
        ConsoleEx.Write(" ...", ConsoleColor.Black);
        DisplayGroupStrengthChanges(gameAction.GroupStrengthChanges);
        _pressAnyKeyControl.Show();
    }

    private void DisplayPopularityChanges(string groupPopularityChanges)
    {
        int line = 6;

        for (int i = 0; i < 8; i++)
        {
            if (groupPopularityChanges[i] != 'M')
            {
                int popularityChange = groupPopularityChanges[i] - 'M';

                ConsoleEx.WriteAt(1, line, $"  {_groupService.GetGroupNameByIndex(i)}", ConsoleColor.Black);
                ConsoleEx.WriteAt(22, line, $"{GetFormattedChange(popularityChange)}", ConsoleColor.Black);
                line++;
            }
        }
    }

    private void DisplayGroupStrengthChanges(string groupStrengthChanges)
    {
        int line = Console.CursorTop + 3;

        for (int i = 0; i < 6; i++)
        {
            if (groupStrengthChanges[i] != 'M')
            {
                int strengthChange = groupStrengthChanges[i] - 'M';

                ConsoleEx.WriteAt(1, line, $"  {_groupService.GetGroupNameByIndex(i)}", ConsoleColor.Black);
                ConsoleEx.WriteAt(22, line, $"{GetFormattedChange(strengthChange)}", ConsoleColor.Black);
                line++;
            }
        }
    }

    private string GetFormattedChange(int change)
    {
        if (change > 0)
        {
            return "+" + change;
        }

        return change.ToString();
    }
}
