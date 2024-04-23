using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.PresidentialDecision;

/// <summary>
///     Represents the dialog that is displayed when the player is given a list of options to make a
///     presidential decision.
/// </summary>
public interface IPresidentialDecisionMainDialog
{
    /// <summary>
    ///     Displays the dialog.
    /// </summary>
    /// <returns>The type of presidential decision that has been selected.</returns>
    public DecisionType Show();
}

/// <summary>
///     Represents the dialog that is displayed when the player is given a list of options to make a
///     presidential decision.
/// </summary>
public class PresidentialDecisionMainDialog : BaseScreen, IPresidentialDecisionMainDialog
{
    private readonly IPressAnyKeyOrOptionControl _pressAnyKeyOrOptionControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PresidentialDecisionMainDialog"/> class from a 
    ///     <see cref="IPressAnyKeyOrOptionControl"/> component.
    /// </summary>
    /// <param name="pressAnyKeyOrOptionControl">The control that is displayed when the user is required to press a key
    /// or select an option.</param>
    public PresidentialDecisionMainDialog(IConsoleService consoleService, IPressAnyKeyOrOptionControl pressAnyKeyOrOptionControl)
        : base(consoleService)
    {
        _pressAnyKeyOrOptionControl = pressAnyKeyOrOptionControl;
    }

    /// <summary>
    ///     Displays the dialog.
    /// </summary>
    /// <returns>The type of presidential decision that has been selected.</returns>
    public DecisionType Show()
    {
        _consoleService.Clear('*', ConsoleColor.Red, ConsoleColor.Yellow);
        _consoleService.PrintAt(3, 5, "PRESIDENTIAL DECISION", ConsoleColor.Blue, ConsoleColor.White);
        _consoleService.PrintAt(6, 1, "Try to ...", ConsoleColor.Yellow, ConsoleColor.Black);
        _consoleService.PrintAt(8, 4, "1. PLEASE a GROUP       ", ConsoleColor.Yellow, ConsoleColor.Black);
        _consoleService.PrintAt(10, 4, "2. PLEASE ALL GROUPS    ", ConsoleColor.Yellow, ConsoleColor.Black);
        _consoleService.PrintAt(12, 4, "3. IMPROVE your CHANCES ", ConsoleColor.Yellow, ConsoleColor.Black);
        _consoleService.PrintAt(14, 4, "4. RAISE some CASH      ", ConsoleColor.Yellow, ConsoleColor.Black);
        _consoleService.PrintAt(16, 4, "5. STRENGTHEN a GROUP   ", ConsoleColor.Yellow, ConsoleColor.Black);
        
        ConsoleKey keyPressed = _pressAnyKeyOrOptionControl.Show();

        switch (keyPressed)
        {
            case ConsoleKey.D1:
                return DecisionType.PleaseAGroup;
            case ConsoleKey.D2:
                return DecisionType.PleaseAllGroups;
            case ConsoleKey.D3:
                return DecisionType.ImproveYourChanges;
            case ConsoleKey.D4:
                return DecisionType.RaiseSomeCash;
            case ConsoleKey.D5:
                return DecisionType.StrengthenAGroup;
            default:
                return DecisionType.None;
        }
    }
}
