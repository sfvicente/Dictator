using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.PresidentialDecision;

/// <summary>
///     Represents the dialog that is displayed when the player needs to make a decision to accept or refuse
///     the presidential option that has been selected.
/// </summary>
public interface IPresidentialDecisionActionDialog
{
    /// <summary>
    ///     Displays the dialog.
    /// </summary>
    /// <param name="decision">The decision to be displayed to the player for acceptance or refusal.</param>
    /// <returns>The option selected after the dialog has been presented.</returns>
    DialogResult Show(Decision decision);

    /// <summary>
    ///     Displays the insufficient funds notice when the treasury cannot cover the selected decision.
    /// </summary>
    void ShowInsufficientFunds(Decision decision, bool isRussianLoanAvailable, bool isAmericanLoanAvailable);
}

/// <summary>
///     Represents the dialog that is displayed when the player needs to make a decision to accept or refuse
///     the presidential option that has been selected.
/// </summary>
public class PresidentialDecisionActionDialog : BaseScreen, IPresidentialDecisionActionDialog
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;
    private readonly IPressAnyKeyWithYesControl _pressAnyKeyWithYesControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PresidentialDecisionActionDialog"/> class from a 
    ///     <see cref="IPressAnyKeyWithYesControl"/> component.
    /// </summary>
    /// <param name="pressAnyKeyWithYesControl">The control that is displayed when the user is required to press a key
    /// or select yes.</param>
    public PresidentialDecisionActionDialog(
        IConsoleService consoleService,
        IPressAnyKeyControl pressAnyKeyControl,
        IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        : base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
        _pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
    }

    public void ShowInsufficientFunds(Decision decision, bool isRussianLoanAvailable, bool isAmericanLoanAvailable)
    {
        _consoleService.Clear(ConsoleColor.DarkYellow, ConsoleColor.Black);
        _consoleService.WriteAt(1, 6, $"{decision.Text}", ConsoleColor.DarkYellow, ConsoleColor.Black);
        _consoleService.WriteAt(3, 9, "The CASH for this decision is", ConsoleColor.DarkYellow, ConsoleColor.Black);
        _consoleService.WriteAt(5, 11, " NOT in the TREASURY", ConsoleColor.DarkYellow, ConsoleColor.Black);

        int row = 14;
        if (isRussianLoanAvailable)
        {
            _consoleService.WriteAt(1, row, " Perhaps the RUSSIANS can help ?", ConsoleColor.DarkYellow, ConsoleColor.Black);
            row += 2;
        }

        if (isAmericanLoanAvailable)
        {
            _consoleService.WriteAt(1, row, "THE AMERICANS are generous folk ", ConsoleColor.DarkYellow, ConsoleColor.Black);
        }

        _pressAnyKeyControl.Show();
    }

    /// <summary>
    ///     Displays the dialog.
    /// </summary>
    /// <param name="decision">The decision to be displayed to the player for acceptance or refusal.</param>
    /// <returns>The option selected after the dialog has been presented.</returns>
    public DialogResult Show(Decision decision)
    {
        _consoleService.Clear(ConsoleColor.DarkYellow);
        _consoleService.WriteAt(1, 5, $"{decision.Text}", ConsoleColor.Yellow, ConsoleColor.Black);

        _consoleService.SetBackgroundColor(ConsoleColor.DarkYellow);

        if (decision.Cost == 0 && decision.MonthlyCost == 0)
        {
            _consoleService.WriteAt(1, 11, "        NO MONEY INVOLVED       ", ConsoleColor.Black);
        }
        else
        {
            _consoleService.WriteAt(2, 10, "This decision would", ConsoleColor.Black);

            if (decision.Cost != 0)
            {
                string addOrTake = (decision.Cost > 0) ? "ADD to" : "TAKE from";

                _consoleService.WriteAt(2, 12, $"{addOrTake} the TREASURY ${Math.Abs(decision.Cost)},000", ConsoleColor.Black);
            }

            if (decision.Cost != 0 && decision.MonthlyCost != 0)
            {
                _consoleService.WriteAt(2, 14, "and", ConsoleColor.Black);
            }

            if (decision.MonthlyCost != 0)
            {
                string raiseOrLower = (decision.MonthlyCost < 0) ? "RAISE" : "LOWER";

                _consoleService.WriteAt(2, 16, $"{raiseOrLower} MONTHLY COSTS by ${Math.Abs(decision.MonthlyCost)},000", ConsoleColor.Black);
            }
        }

        return _pressAnyKeyWithYesControl.Show();
    }
}
