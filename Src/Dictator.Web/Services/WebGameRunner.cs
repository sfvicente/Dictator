using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using Dictator.Core.Models;
using Microsoft.JSInterop;

namespace Dictator.Web.Services;

public class WebGameRunner
{
    private readonly IEngine _engine;
    private readonly IUserInterface _userInterface;
    private readonly WebSpectrumConsoleService _console;
    private readonly IJSRuntime _js;

    public WebGameRunner(
        IEngine engine,
        IUserInterface userInterface,
        WebSpectrumConsoleService console,
        IJSRuntime js)
    {
        _engine = engine;
        _userInterface = userInterface;
        _console = console;
        _js = js;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        _userInterface.Initialise();

        while (!cancellationToken.IsCancellationRequested)
        {
            _engine.Initialise();

            _userInterface.DisplayIntroScreen();
            await _console.WaitForKeyAsync();

            _userInterface.DisplayTitleScreen();
            await PlayEffectSafeAsync("startAnthem", isDirectMethod: true);
            await _console.WaitForKeyAsync();
            await PlayEffectSafeAsync("stopAnthem", isDirectMethod: true);

            int storedHighScore = await GetStoredHighScoreAsync();
            int engineHighScore = Math.Max(_engine.GetHighScore(), storedHighScore);
            _userInterface.DisplayWelcomeScreen(engineHighScore);
            await _console.WaitForKeyAsync();

            _userInterface.DisplayAccountScreen(_engine.GetAccount());
            await _console.WaitForKeyAsync();

            _userInterface.DisplayPoliceReportScreen(_engine.GetPoliceReport());
            await _console.WaitForKeyAsync();

            while (!cancellationToken.IsCancellationRequested)
            {
                _engine.SetMonthlyMinimalPopularityAndStrength();
                _engine.SetMonthlyRevolutionStrength();

                _engine.AdvanceMonth();
                _userInterface.DisplayMonthScreen(_engine.GetMonth());
                await _console.WaitForKeyAsync();

                _engine.Plot();
                await ProcessTreasuryMonthlyCostsAsync();
                await TriggerAndHandleAudienceRequestAsync();
                _engine.Plot();

                if (await TryProcessAssassinationAttemptAsync() || await TryProcessConflictAsync())
                {
                    await ProcessEndAsync();
                    break;
                }

                _engine.Plot();
                await ProcessPoliceReportAsync();
                await HandlePresidentialDecisionAsync();
                await ProcessPoliceReportAsync();

                if (_engine.ShouldNewsHappen())
                {
                    await ExecuteRandomNewsflashAsync();
                    _engine.Plot();
                    await ProcessPoliceReportAsync();
                }

                if (_engine.TryTriggerRevoltGroup())
                {
                    if (await TryProcessGovernmentOverthrownAsync())
                    {
                        await ProcessEndAsync();
                        break;
                    }
                }
            }
        }
    }

    private async Task ProcessTreasuryMonthlyCostsAsync()
    {
        _engine.PayMonthlyCosts();

        if (_engine.IsGovernmentBankrupt())
        {
            _userInterface.DisplayBankruptcyScreen();
            await _console.WaitForKeyAsync();

            _engine.ApplyBankruptcyEffects();
            _engine.Plot();
            await ProcessPoliceReportAsync();
        }
    }

    private async Task TriggerAndHandleAudienceRequestAsync()
    {
        Audience audience = _engine.SelectRandomUnusedAudienceRequest();

        _userInterface.DisplayAudienceScreen(audience);
        await _console.WaitForKeyAsync();

        if (await AskYesNoDialogAsync(() => _userInterface.DisplayAdviceRequestDialog()))
        {
            _userInterface.DisplayAdviceScreen(audience);
            await _console.WaitForKeyAsync();
        }

        if (await AskYesNoDialogAsync(() => _userInterface.DisplayAudienceDecisionDialog(audience)))
        {
            _engine.AcceptAudienceRequest(audience);
        }
        else
        {
            _engine.RefuseAudienceRequest(audience);
        }

        _userInterface.DisplayAccountScreen(_engine.GetAccount());
        await _console.WaitForKeyAsync();
    }

    private async Task ProcessPoliceReportAsync()
    {
        PoliceReportRequest request = _engine.RequestPoliceReport();
        bool canAffordAndAvailable = request.HasEnoughBalance &&
                                     request.IsPlayerPopularWithSecretPolice &&
                                     request.HasPoliceEnoughStrength;

        _userInterface.DisplayPoliceReportRequestDialog(request);
        ConsoleKey key = await _console.WaitForKeyAsync();

        if (canAffordAndAvailable && key == ConsoleKey.Y)
        {
            _engine.PayFromTreasury(1);
            _userInterface.DisplayPoliceReportScreen(_engine.GetPoliceReport());
            await _console.WaitForKeyAsync();
        }
    }

    private async Task<bool> TryProcessAssassinationAttemptAsync()
    {
        var (shouldHappen, assassinGroupType) = _engine.ShouldAssassinationAttemptHappen();

        if (shouldHappen && assassinGroupType.HasValue)
        {
            string groupName = _engine.GetAssassinationGroupName(assassinGroupType.Value);

            _userInterface.DisplayAssassinationAttempt(groupName);
            await _console.WaitForKeyAsync();

            if (_engine.IsAssassinationSuccessful())
            {
                _userInterface.DisplayAssassinationSucceededScreen();
                await PlayEffectSafeAsync("dead");
                await _console.WaitForKeyAsync();
                _engine.KillPlayer();
                return true;
            }

            _userInterface.DisplayAssassinationFailedScreen();
            await _console.WaitForKeyAsync();
        }

        return false;
    }

    private async Task<bool> TryProcessConflictAsync()
    {
        if (!_engine.DoesConflictExist())
        {
            return false;
        }

        if (_engine.ShouldWarHappen())
        {
            WarStats warStats = _engine.BeginInvasion();

            _userInterface.DisplayLeftotoInvadesScreen(warStats);
            await _console.WaitForKeyAsync();

            bool hasWarBeenWon = _engine.ExecuteWar(warStats);

            if (hasWarBeenWon)
            {
                _userInterface.DisplayWarWonScreen();
                await _console.WaitForKeyAsync();
                _engine.SetGroupStrength(GroupType.Leftotans, 0);
                return false;
            }

            await ProcessWarLostScenarioAsync();
            return true;
        }

        _userInterface.DisplayWarThreatScreen();
        await _console.WaitForKeyAsync();
        _engine.ApplyThreatOfWarEffects();
        return false;
    }

    private async Task ProcessWarLostScenarioAsync()
    {
        _userInterface.DisplayWarLostScreen();
        await _console.WaitForKeyAsync();

        if (_engine.HasPlayerPurchasedHelicopter())
        {
            if (_engine.IsPlayerAbleToEscapeAfterLosingWar())
            {
                _userInterface.DisplayHelicopterEscapeScreen();
                await _console.WaitForKeyAsync();
            }
            else
            {
                _userInterface.DisplayHelicopterEngineFailure();
                await _console.WaitForKeyAsync();
                _engine.KillPlayer();
            }
        }
        else
        {
            _userInterface.DisplayWarExecutionScreen();
            await _console.WaitForKeyAsync();
            _engine.KillPlayer();
        }
    }

    private async Task HandlePresidentialDecisionAsync()
    {
        bool hasNotChosenDecisionOrExited = true;

        while (hasNotChosenDecisionOrExited)
        {
            _userInterface.DisplayPresidentialDecisionMainDialog();
            ConsoleKey mainKey = await _console.WaitForKeyAsync();

            DecisionType decisionType = mainKey switch
            {
                ConsoleKey.D1 => DecisionType.PleaseAGroup,
                ConsoleKey.D2 => DecisionType.PleaseAllGroups,
                ConsoleKey.D3 => DecisionType.ImproveYourChances,
                ConsoleKey.D4 => DecisionType.RaiseSomeCash,
                ConsoleKey.D5 => DecisionType.StrengthenAGroup,
                _ => DecisionType.None
            };

            if (decisionType == DecisionType.None)
            {
                hasNotChosenDecisionOrExited = false;
            }
            else
            {
                Decision[] decisions = _engine.GetDecisionsByType(decisionType);
                _userInterface.DisplayPresidentialDecisionSubDialog(decisions);
                ConsoleKey subKey = await _console.WaitForKeyAsync();

                int optionSelected = subKey switch
                {
                    ConsoleKey.D1 => 1,
                    ConsoleKey.D2 => 2,
                    ConsoleKey.D3 => 3,
                    ConsoleKey.D4 => 4,
                    ConsoleKey.D5 => 5,
                    ConsoleKey.D6 => 6,
                    _ => 99
                };

                if (_engine.DoesPresidentialOptionExistAndIsAvailable(decisionType, optionSelected))
                {
                    Decision decision = _engine.GetDecisionByTypeAndIndex(decisionType, optionSelected);

                    if (await TryProcessSelectedDecisionAsync(decision))
                    {
                        hasNotChosenDecisionOrExited = false;
                    }
                }
            }
        }
    }

    private async Task<bool> TryProcessSelectedDecisionAsync(Decision decision)
    {
        if (decision.DecisionSubType == DecisionSubType.TransferToSwissAccount)
        {
            SwissBankAccountTransfer transfer = _engine.TransferToSwissBankAccount();
            Account account = _engine.GetAccount();

            _userInterface.DisplayTransferToSwissBankAccount(transfer, account);
            await _console.WaitForKeyAsync();
            return true;
        }

        if (decision.DecisionSubType == DecisionSubType.AskForAmericanLoan)
        {
            await AskForLoanAsync(LenderCountry.America);
            return true;
        }

        if (decision.DecisionSubType == DecisionSubType.AskForRussianLoan)
        {
            await AskForLoanAsync(LenderCountry.Russia);
            return true;
        }

        if (await AskYesNoDialogAsync(() => _userInterface.DisplayAdviceRequestDialog()))
        {
            _userInterface.DisplayAdviceScreen(decision);
            await _console.WaitForKeyAsync();
        }

        if (await AskYesNoDialogAsync(() => _userInterface.DisplayPresidentialDecisionActionDialog(decision)))
        {
            if (decision.DecisionSubType == DecisionSubType.IncreaseBodyGuard)
            {
                _engine.IncreaseBodyguard();
            }
            else
            {
                if (decision.DecisionSubType == DecisionSubType.PurchaseHelicopter)
                {
                    _engine.PurchasedHelicopter();
                }

                _engine.MarkDecisionAsUsed(decision.Text);
            }

            _engine.ApplyDecisionEffects(decision);
            return true;
        }

        return false;
    }

    private async Task AskForLoanAsync(LenderCountry country)
    {
        _userInterface.DisplayLoanApplicationScreen();
        string tuneEffect = country == LenderCountry.Russia ? "russianAid" : "americanAid";
        await PlayEffectSafeAsync(tuneEffect);
        await _console.WaitForKeyAsync();

        LoanApplicationResult result = _engine.AskForLoan(country);
        _userInterface.DisplayLoanApplicationResultScreen(result);
        await _console.WaitForKeyAsync();
    }

    private async Task ExecuteRandomNewsflashAsync()
    {
        News unusedRandomNews = _engine.SelectRandomUnusedNews();

        _userInterface.DisplayNewsScreen(unusedRandomNews.Text);
        await PlayEffectSafeAsync("newsflash");
        await _console.WaitForKeyAsync();
        _engine.ApplyNewsEffects(unusedRandomNews);
    }

    private async Task<bool> TryProcessGovernmentOverthrownAsync()
    {
        _userInterface.DisplayRevolutionScreen();
        await PlayEffectSafeAsync("revolution");
        await _console.WaitForKeyAsync();

        if (await AskYesNoDialogAsync(() => _userInterface.DisplayEscapeAttemptDialog()))
        {
            await AttemptEscapeAsync();
            return true;
        }

        Dictionary<int, Group> possibleAllies = _engine.FindPossibleAlliesForPlayer();

        if (possibleAllies.Count > 0)
        {
            Revolutionary revolutionary = _engine.GetRevolutionary();
            int selectedAllyGroupId = await ShowRevolutionAskForHelpDialogAsync(revolutionary, possibleAllies);

            if (_engine.DoesGroupAcceptAllianceInRevolution(selectedAllyGroupId))
            {
                _engine.SetPlayerAllyForRevolution(selectedAllyGroupId);
            }
            else
            {
                _userInterface.DisplayRevolutionAllyLowPopularityScreen();
                await _console.WaitForKeyAsync();
            }
        }
        else
        {
            _userInterface.DisplayRevolutionNoAlliesScreen();
            await _console.WaitForKeyAsync();
        }

        _userInterface.DisplayRevolutionStartedScreen();
        await _console.WaitForKeyAsync();

        if (_engine.DoesRevolutionSucceed())
        {
            _userInterface.DisplayRevolutionOverthrownScreen();
            await _console.WaitForKeyAsync();
            _engine.KillPlayer();
            return true;
        }

        if (await AskYesNoDialogAsync(() => _userInterface.DisplayRevolutionCrushedDialog()))
        {
            _engine.PunishRevolutionaries();
        }

        _engine.ApplyRevolutionCrushedEffects();
        return false;
    }

    private async Task<int> ShowRevolutionAskForHelpDialogAsync(Revolutionary revolutionary, Dictionary<int, Group> possibleAllies)
    {
        _console.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        _console.WriteCenteredAt(5, $"{revolutionary.RevolutionaryGroupName} have joined with");
        _console.WriteCenteredAt(6, $"{revolutionary.RevolutionaryGroupAllyName}");
        _console.WriteAt(1, 7, $"  Their combined STRENGTH is {revolutionary.CombinedStrength} ");
        _console.WriteAt(1, 9, "  WHO are you ASKING for HELP ? ");

        for (int i = 1; i < 7; i++)
        {
            if (possibleAllies.TryGetValue(i, out Group? value))
            {
                _console.WriteAt(6, 14 + i, $"{i} {value.Name}");
            }
            else
            {
                _console.WriteEmptyLineAt(14 + i);
            }
        }

        while (true)
        {
            ConsoleKey keyPressed = await _console.WaitForKeyAsync();
            if (keyPressed >= ConsoleKey.D1 && keyPressed <= ConsoleKey.D6)
            {
                int optionNumber = (int)keyPressed - (int)ConsoleKey.D0;
                if (possibleAllies.ContainsKey(optionNumber))
                {
                    return optionNumber;
                }
            }
        }
    }

    private async Task AttemptEscapeAsync()
    {
        if (_engine.HasPlayerPurchasedHelicopter())
        {
            if (_engine.IsPlayerAbleToEscapeByHelicopter())
            {
                _userInterface.DisplayHelicopterEscapeScreen();
                await _console.WaitForKeyAsync();
            }
            else
            {
                _userInterface.DisplayHelicopterWontStartScreen();
                await _console.WaitForKeyAsync();
                await EscapeToLeftotoAsync();
            }
        }
        else
        {
            await EscapeToLeftotoAsync();
        }
    }

    private async Task EscapeToLeftotoAsync()
    {
        _userInterface.DisplayEscapeToLeftotoScreen();
        await _console.WaitForKeyAsync();

        if (_engine.DoesGuerrillaCatchPlayerEscaping())
        {
            _userInterface.DisplayGuerillasCelebratingScreen();
            await _console.WaitForKeyAsync();
            _engine.KillPlayer();
        }
        else
        {
            _userInterface.DisplayGuerillasMissedScreen();
            await _console.WaitForKeyAsync();
        }
    }

    private async Task ProcessEndAsync()
    {
        Score score = _engine.GetCurrentScore();
        int storedHighScore = await GetStoredHighScoreAsync();
        if (storedHighScore > score.HighestScore)
        {
            score.HighestScore = storedHighScore;
        }

        if (score.PointsForStayingAlive == 0)
        {
            await PlayEffectSafeAsync("funeral");
        }

        _userInterface.DisplayEndScreen(score);
        await _console.WaitForKeyAsync();
        _engine.SaveHighScore();
        await SaveStoredHighScoreAsync(Math.Max(score.TotalScore, score.HighestScore));

        // Line 3090-3092: Show FINAL Police Report after the rating screen
        _userInterface.DisplayPoliceReportScreen(_engine.GetPoliceReport());
        _console.SetFlash(true);
        _console.WriteAt(15, 2, "FINAL", ConsoleColor.Black, ConsoleColor.White);
        _console.SetFlash(false);
        await _console.WaitForKeyAsync();
    }

    private async Task<bool> AskYesNoDialogAsync(Action renderDialog)
    {
        renderDialog();
        ConsoleKey key = await _console.WaitForKeyAsync();
        return key == ConsoleKey.Y;
    }

    private async Task PlayEffectSafeAsync(string name, bool isDirectMethod = false)
    {
        try
        {
            if (isDirectMethod)
            {
                await _js.InvokeVoidAsync($"spectrumDisplay.{name}");
            }
            else
            {
                await _js.InvokeVoidAsync("spectrumDisplay.playEffect", name);
            }
        }
        catch
        {
            // Ignore audio interop errors
        }
    }

    private async Task<int> GetStoredHighScoreAsync()
    {
        try
        {
            return await _js.InvokeAsync<int>("spectrumDisplay.getHighScore");
        }
        catch
        {
            return 0;
        }
    }

    private async Task SaveStoredHighScoreAsync(int score)
    {
        try
        {
            await _js.InvokeVoidAsync("spectrumDisplay.setHighScore", score);
        }
        catch
        {
            // Ignore storage interop errors
        }
    }
}
