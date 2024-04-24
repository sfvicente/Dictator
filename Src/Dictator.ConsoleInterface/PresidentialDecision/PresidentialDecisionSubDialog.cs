using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.PresidentialDecision;

/// <summary>
///     Represents the dialog that is displayed when the player is given the list of sub-options
///     within a specific option to make a presidential decision.
/// </summary>
public interface IPresidentialDecisionSubDialog
{
    public int Show(Decision[] decisions);
}

/// <summary>
///     Represents the dialog that is displayed when the player is given the list of sub-options
///     within a specific option to make a presidential decision.
/// </summary>
public class PresidentialDecisionSubDialog : BaseScreen, IPresidentialDecisionSubDialog
{
    private readonly IPressAnyKeyOrOptionControl _pressAnyKeyOrOptionControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PresidentialDecisionSubDialog"/> class from a 
    ///     <see cref="IPressAnyKeyOrOptionControl"/> component.
    /// </summary>
    /// <param name="pressAnyKeyOrOptionControl">The control that is displayed when the user is required to press a key
    /// or select an option.</param>
    public PresidentialDecisionSubDialog(IConsoleService consoleService, IPressAnyKeyOrOptionControl pressAnyKeyOrOptionControl)
        : base(consoleService)
    {
        _pressAnyKeyOrOptionControl = pressAnyKeyOrOptionControl;
    }

    private bool HaveAllDecisionsBeenUsed(Decision[] decisions)
    {
        for (int i = 0; i < decisions.Length; i++)
        {
            if (!decisions[i].HasBeenUsed)
            {
                return false;
            }
        }

        return true;
    }

    public int Show(Decision[] decisions)
    {
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        _consoleService.Clear();

        if(HaveAllDecisionsBeenUsed(decisions))
        {
            _consoleService.PrintAt(12, 3, "ALL of this section USED UP");
        }
        else
        {
            // TODO: Display options with the original positioning
            int line = 4;

            for (int i = 0; i < decisions.Length; i++)
            {
                int optionNumber = i + 1;

                if (!decisions[i].HasBeenUsed)
                {
                    _consoleService.WriteAt(1, line + i, $"{optionNumber}.");
                }

                line++;

                if (!decisions[i].HasBeenUsed)
                {
                    _consoleService.WriteAt(1, line + i, decisions[i].Text);
                }

                line++;
            }
        }
        
        ConsoleKey keyPressed = _pressAnyKeyOrOptionControl.Show();

        switch (keyPressed)
        {
            case ConsoleKey.D1:
                return 1;
            case ConsoleKey.D2:
                return 2;
            case ConsoleKey.D3:
                return 3;
            case ConsoleKey.D4:
                return 4;
            case ConsoleKey.D5:
                return 5;
            case ConsoleKey.D6:
                return 6;
            default:
                return 99;
        }
    }
}
