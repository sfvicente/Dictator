﻿using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Treasury;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.PresidentialDecision;

/// <summary>
///     Represents the screen that is displayed when the player transfers funds from the treasury 
///     to the Swiss bank account.
/// </summary>
public interface ITransferToSwissBankAccountScreen
{
    /// <summary>
    ///     Displays the screen.
    /// </summary>
    /// <param name="swissBankAccountTransfer">The details of the Swiss account transfer.</param>
    /// <param name="account">The current treasury and costs information.</param>
    public void Show(SwissBankAccountTransfer swissBankAccountTransfer, Account account);
}

/// <summary>
///     Represents the screen that is displayed when the player transfers funds from the treasury 
///     to the Swiss bank account.
/// </summary>
public class TransferToSwissBankAccountScreen : BaseScreen, ITransferToSwissBankAccountScreen
{
    private readonly IAccountControl _accountControl;
    private readonly IPressAnyKeyControl _pressAnyKeyControl;

    public TransferToSwissBankAccountScreen(
        IConsoleService consoleService, IAccountControl accountControl, IPressAnyKeyControl pressAnyKeyControl)
        : base(consoleService)
    {
        _accountControl = accountControl;
        _pressAnyKeyControl = pressAnyKeyControl;
    }

    /// <summary>
    ///     Displays the screen.
    /// </summary>
    /// <param name="swissBankAccountTransfer">The details of the Swiss account transfer.</param>
    /// <param name="account">The current treasury and costs information.</param>
    public void Show(SwissBankAccountTransfer swissBankAccountTransfer, Account account)
    {
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.ForegroundColor = ConsoleColor.Black;
        _consoleService.Clear();
        _consoleService.WriteAt(1, 4, "TRANSFER to a SWISS BANK ACCOUNT", ConsoleColor.Black, ConsoleColor.DarkYellow);

        if (swissBankAccountTransfer.AmountStolen > 0)
        {
            _consoleService.WriteAt(1, 7, $"The TREASURY held ${swissBankAccountTransfer.TreasuryPreviousBalance},000");
            _consoleService.WriteAt(1, 10, $"${swissBankAccountTransfer.AmountStolen},000 has been TRANSFERRED");
        }
        else
        {
            _consoleService.WriteAt(12, 7, "NO TRANSFER made");
        }

        _accountControl.Show(account);
        _pressAnyKeyControl.Show();
    }
}
