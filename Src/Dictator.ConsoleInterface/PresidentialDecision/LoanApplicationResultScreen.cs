﻿using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;

namespace Dictator.ConsoleInterface.PresidentialDecision;

/// <summary>
///     Represents the screen that is displayed when the result of an application 
///     for monetary foreign aid that a player is determined.
/// </summary>
public interface ILoanApplicationResultScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    /// <param name="loanApplicationResult">The load application result to be displayed on screen.</param>
    public void Show(LoanApplicationResult loanApplicationResult);
}

/// <summary>
///     Represents the screen that is displayed when the result of an application 
///     for monetary foreign aid that a player is determined.
/// </summary>
public class LoanApplicationResultScreen : BaseScreen, ILoanApplicationResultScreen
{
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LoanApplicationResultScreen"/> class from a <see cref="IPressAnyKeyControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
    public LoanApplicationResultScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
    {
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    /// <param name="loanApplicationResult">The load application result to be displayed on screen.</param>
    public void Show(LoanApplicationResult loanApplicationResult)
    {
        if(loanApplicationResult.IsAccepted)
        {
            _consoleService.WriteAt(1, 12, $"{loanApplicationResult.GroupName} will let you have");
            _consoleService.WriteAt(8, 14, $"{loanApplicationResult.Amount},000 DOLLARS");
        }
        else
        {
            if(loanApplicationResult.RefusalType == LoanApplicationRefusalType.TooEarly)
            {
                _consoleService.WriteAt(2, 12, "It's TOO EARLY to give AID");
            }
            else if(loanApplicationResult.RefusalType == LoanApplicationRefusalType.AlreadyUsed)
            {
                _consoleService.WriteAt(2, 12, "Very sorry, NO MORE LOANS");
            }
            else if(loanApplicationResult.RefusalType == LoanApplicationRefusalType.NotPopularEnough)
            {
                if (loanApplicationResult.Country == LenderCountry.America)
                {
                    _consoleService.WriteAt(12, 12, "\"nuts !\"");
                }
                else if (loanApplicationResult.Country == LenderCountry.Russia)
                {
                    _consoleService.WriteAt(12, 12, "NIET !");
                }
            }
        }

        _pressAnyKeyControl.Show();
    }
}
