using System.Collections.Generic;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using Dictator.Core.Models;

namespace Dictator.ConsoleInterface;

public class Game
{
    private readonly IEngine _engine;
    private readonly IUserInterface _userInterface;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Game"/> class from an <see cref="IEngine"/> and a <see cref="IUserInterface"/> components.
    /// </summary>
    /// <param name="engine">The engine component responsible for the coordination of business calls to the services.</param>
    /// <param name="userInterface">The user interface component responsible for displaying the screens.</param>
    public Game(IEngine engine, IUserInterface userInterface)
    {
        _engine = engine;
        _userInterface = userInterface;
    }

    /// <summary>
    ///     Starts a game cycle which initialises the interface and processes the game flow.
    /// </summary>
    public void Start()
    {
        _userInterface.Initialise();

        while (true)
        {
            _engine.Initialise();
            _userInterface.DisplayIntroScreen();
            _userInterface.DisplayTitleScreen();
            DisplayWelcomeScreen();
            DisplayAccountDetails();
            DisplayPoliceReportScreen();

            while (true)
            {
                _engine.SetMonthlyMinimalPopularityAndStrength();
                _engine.SetMonthlyRevolutionStrength();
                AdvanceAndDisplayCurrentMonth();
                _engine.Plot();
                ProcessTreasuryMonthlyCosts();
                HandleAudienceRequest();
                _engine.Plot();

                if (TryProcessAssassinationAttempt() || TryProcessConflict())
                {
                    ProcessEnd();
                    break;
                }

                _engine.Plot();
                ProcessPoliceReport();
                HandlePresidentialDecision();
                ProcessPoliceReport();
                
                if(_engine.ShouldNewsHappen())
                {
                    ExecuteRandomNewsflash();
                    _engine.Plot();
                    ProcessPoliceReport();
                }

                if (TryTriggerRevolution())
                {
                    if (TryProcessGovernmentOverthrown())
                    {
                        ProcessEnd();
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    ///     Displays the welcome screen.
    /// </summary>
    private void DisplayWelcomeScreen()
    {
        int highscore = _engine.GetHighScore();

        _userInterface.DisplayWelcomeScreen(highscore);
    }

    /// <summary>
    ///     Advances the current month and displays it on the month screen.
    /// </summary>
    private void AdvanceAndDisplayCurrentMonth()
    {
        _engine.AdvanceMonth();

        int currentMonth = _engine.GetMonth();

        _userInterface.DisplayMonthScreen(currentMonth);
    }

    /// <summary>
    ///     Handles the payment of the monthly costs and the check for bankruptcy.
    /// </summary>
    private void ProcessTreasuryMonthlyCosts()
    {
        _engine.PayMonthlyCosts();

        if (_engine.IsGovernmentBankrupt())
        {
            _userInterface.DisplayBankuptcyScreen();
            _engine.ApplyBankruptcyEffects();
            _engine.Plot();  // Realculate probability for the assassination and revolution events
            ProcessPoliceReport();
        }
    }

    /// <summary>
    ///     Displays the account details screen.
    /// </summary>
    private void DisplayAccountDetails()
    {
        Account account = _engine.GetAccount();

        _userInterface.DisplayAccountScreen(account);
    }

    /// <summary>
    ///     Displays the police report screen.
    /// </summary>
    private void DisplayPoliceReportScreen()
    {
        PoliceReport policeReport = _engine.GetPoliceReport();

        _userInterface.DisplayPoliceReportScreen(policeReport);
    }

    /// <summary>
    ///     Handles the generation and processing of an audience request from one of the groups.
    /// </summary>
    private void HandleAudienceRequest()
    {
        Audience audience = _engine.SelectRandomUnusedAudienceRequest();

        _userInterface.DisplayAudienceScreen(audience);

        if (DoesPlayerAcceptAdvice())
        {
            _userInterface.DisplayAdviceScreen(audience);
        }

        AskForAudienceDecision(audience);
        DisplayAccountDetails();
    }

    /// <summary>
    ///     Determines if the player has chosen to accept advice.
    /// </summary>
    /// <returns><c>true</c> if the player accepted advice; otherwise, <c>false</c>.</returns>
    private bool DoesPlayerAcceptAdvice()
    {
        DialogResult dialogResult = _userInterface.DisplayAdviceRequestDialog();

        if (dialogResult == DialogResult.Yes)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Asks the player to make a decision on a request made by one of the groups. The player
    ///     can either accept or refuse the request.
    /// </summary>
    /// <param name="audience">The audience that the player will be asked to make a decision on.</param>
    private void AskForAudienceDecision(Audience audience)
    {
        DialogResult dialogResult = _userInterface.DisplayAudienceDecisionDialog(audience);

        if (dialogResult == DialogResult.Yes)
        {
            _engine.AcceptAudienceRequest(audience);
        }
        else
        {
            _engine.RefuseAudienceRequest(audience);
        }
    }

    /// <summary>
    ///     Asks the player if the police report should be displayed and if the user accepts, displays it.
    /// </summary>
    private void ProcessPoliceReport()
    {
        PoliceReportRequest policeReportRequest = _engine.RequestPoliceReport();
        DialogResult dialogResult = _userInterface.DisplayPoliceReportRequestDialog(policeReportRequest);

        if (dialogResult == DialogResult.Yes)
        {
            _engine.PayFromTreasury(1);
            DisplayPoliceReportScreen();
        }
    }

    /// <summary>
    ///     Tries to process an assassination attempt, determining if it should happen and then if it is successful.
    /// </summary>
    /// <returns><c>true</c> if an assassination is processed and successful that leads to the end of the current game; otherwise, <c>false</c>.</returns>
    private bool TryProcessAssassinationAttempt()
    {
        if (_engine.ShouldAssassinationAttemptHappen())
        {
            string groupName = _engine.GetAssassinationGroupName();

            _userInterface.DisplayAssassinationAttempt(groupName);

            if (_engine.IsAssassinationSuccessful())
            {
                // As attempt was successful, display death screen and kill player as game must end
                _userInterface.DisplayAssassinationSuccededScreen();
                _engine.KillPlayer();
                return true;
            }

            _userInterface.DisplayAssassinationFailedScreen();
        }

        // Either no attempt happened or attempt has failed, so the game should progress
        return false;
    }

    /// <summary>
    ///     Tries to process a conflit, determining if exists and if it might lead to a war scenario.
    /// </summary>
    /// <returns><c>true</c> if a conflict is processed that leads to the end of the current game; otherwise, <c>false</c>.</returns>
    private bool TryProcessConflict()
    {
        bool isGameOver = false;

        if (_engine.DoesConflictExist())
        {
            if (_engine.ShouldWarHappen())
            {
                WarStats warStats = _engine.BeginInvasion();
         
                _userInterface.DisplayLeftotoInvadesScreen(warStats);

                bool hasWarBeenWon = _engine.ExecuteWar(warStats);

                if (hasWarBeenWon)
                {
                    ProcessWarWonScenario();
                }
                else
                {
                    ProcessWarLostScenario();
                    isGameOver = true;
                }
            }
            else
            {
                _userInterface.DisplayWarThreatScreen();
                _engine.ApplyThreatOfWarEffects();
            }
        }

        return isGameOver;
    }

    /// <summary>
    ///     Processes the scenario when the player wins the war.
    /// </summary>
    private void ProcessWarWonScenario()
    {
        _userInterface.DisplayWarWonScreen();
        _engine.SetGroupStrength(GroupType.Leftotans, 0);
    }

    /// <summary>
    ///     Processes the scenario when the player loses the war.
    /// </summary>
    private void ProcessWarLostScenario()
    {
        _userInterface.DisplayWarLostScreen();

        if (_engine.HasPlayerPurchasedHelicopter())
        {
            if (_engine.IsPlayerAbleToEscapeAfterLosingWar())
            {
                _userInterface.DisplayHelicopterEscapeScreen();
            }
            else
            {
                _userInterface.DisplayHelicopterEngineFailure();
                _engine.KillPlayer();
            }
        }
        else
        {
            _userInterface.DisplayWarExecutionScreen();
            _engine.KillPlayer();
        }

        ProcessEnd();
    }

    /// <summary>
    ///     Handles the menu and sub menu selection of the presidential decision until it is executed.
    /// </summary>
    private void HandlePresidentialDecision()
    {
        bool hasNotChosenDecisionOrExited = true;

        while (hasNotChosenDecisionOrExited)
        {
            DecisionType decisionType = _userInterface.DisplayPresidentialDecisionMainDialog();

            if (decisionType == DecisionType.None)
            {
                hasNotChosenDecisionOrExited = false;
            }
            else
            {
                Decision[] decisions = _engine.GetDecisionsByType(decisionType);
                int optionSelected = _userInterface.DisplayPresidentialDecisionSubDialog(decisions);

                if (_engine.DoesPresidentialOptionExistAndIsAvailable(decisionType, optionSelected))
                {
                    Decision decision = _engine.GetDecisionByTypeAndIndex(decisionType, optionSelected);

                    if (TryProcessSelectedDecision(decision))
                    {
                        hasNotChosenDecisionOrExited = false;
                    }
                }
            }
        }
    }

    /// <summary>
    ///     Attempts to process the selected presidential decision taking into account the non-standard decisions such as loan
    ///     increase bodyguard and transfer to Swiss bank account.
    /// </summary>
    /// <param name="decision"></param>
    /// <returns><c>true</c> if a decision has been chosen and executed; otherwise, <c>false</c>.</returns>
    private bool TryProcessSelectedDecision(Decision decision)
    {
        bool hasDecisionBeenCompleted = false;

        if (decision.DecisionSubType == DecisionSubType.TransferToSwissAccount)
        {
            SwissBankAccountTransfer transfer = _engine.TransferToSwissBankAccount();
            Account account = _engine.GetAccount();

            _userInterface.DisplayTransferToSwissBankAccount(transfer, account);
            hasDecisionBeenCompleted = true;
        }
        else if (decision.DecisionSubType == DecisionSubType.AskForAmericanLoan)
        {
            AskForLoan(LenderCountry.America);
            hasDecisionBeenCompleted = true;
        }
        else if (decision.DecisionSubType == DecisionSubType.AskForRussianLoan)
        {
            AskForLoan(LenderCountry.Russia);
            hasDecisionBeenCompleted = true;
        }
        else
        {
            if (DoesPlayerAcceptAdvice())
            {
                _userInterface.DisplayAdviceScreen(decision);
            }

            if (DoesPlayerConfirmDecision(decision))
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
                hasDecisionBeenCompleted = true;
            }
        }

        return hasDecisionBeenCompleted;
    }

    /// <summary>
    ///     Determines if the player confirms the execution of the presidential decision that has been selected.
    /// </summary>
    /// <param name="decision">The presidential decision to confirm.</param>
    /// <returns><c>true</c> if the player confirms the execution of the selected decision; otherwise, <c>false</c>.</returns>
    private bool DoesPlayerConfirmDecision(Decision decision)
    {
        DialogResult dialogResult = _userInterface.DisplayPresidentialDecisionActionDialog(decision);

        if (dialogResult == DialogResult.Yes)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Requests a loan to a foreign country.
    /// </summary>
    /// <param name="country">The country to whom the request is performed.</param>
    private void AskForLoan(LenderCountry country)
    {
        _userInterface.DisplayLoanApplicationScreen();

        LoanApplicationResult loanApplicationResult = _engine.AskForLoan(country);

        _userInterface.DisplayLoanApplicationResultScreen(loanApplicationResult);
    }

    /// <summary>
    ///     Executes a random unused newsflash.
    /// </summary>
    private void ExecuteRandomNewsflash()
    {
        News unusedRandomNews = _engine.SelectRandomUnusedNews();

        _userInterface.DisplayNewsScreen(unusedRandomNews.Text);
        _engine.ApplyNewsEffects(unusedRandomNews);
    }

    /// <summary>
    ///     Attempts to create a revolution scenario by setting one of the groups as a revolutionary element.
    /// </summary>
    /// <returns><c>true</c> if a revolution is triggered; otherwise, <c>false</c>.</returns>
    private bool TryTriggerRevolution()
    {
        bool shouldRevolutionHappen = _engine.TryTriggerRevoltGroup();

        return shouldRevolutionHappen;
    }

    /// <summary>
    ///     Initiates the revolution and attempts to overthrown the government.
    /// </summary>
    /// <returns><c>true</c> if the revolution succeeded and the government has been overthrown; otherwise, <c>false</c>.</returns>
    private bool TryProcessGovernmentOverthrown()
    {
        _userInterface.DisplayRevolutionScreen();

        bool hasGovernmentBeenOverthrown = true;

        if (DoesPlayerAttemptEscape())
        {
            AttemptEscape();
        }
        else
        {
            Dictionary<int, Group> possibleAllies = _engine.FindPossibleAlliesForPlayer();

            if (possibleAllies.Count > 0)
            {
                Revolutionary revolutionary = _engine.GetRevolutionary();
                int selectedAllyGroupId = _userInterface.DisplayRevolutionAskForHelpDialog(revolutionary, possibleAllies);

                if(_engine.DoesGroupAcceptAllianceInRevolution(selectedAllyGroupId))
                {
                    _engine.SetPlayerAllyForRevolution(selectedAllyGroupId);
                }
                else
                {
                    _userInterface.DisplayRevolutionAllyLowPopularityScreen();
                }
            }
            else
            {
                _userInterface.DisplayRevolutionNoAlliesScreen();
            }

            _userInterface.DisplayRevolutionStartedScreen();

            bool doesRevolutionSucceed = _engine.DoesRevolutionSucceed();

            if (doesRevolutionSucceed)
            {
                _userInterface.DisplayRevolutionOverthrownScreen();
                _engine.KillPlayer();
            }
            else
            {
                DialogResult dialogResult = _userInterface.DisplayRevolutionCrushedDialog();

                if (dialogResult == DialogResult.Yes)
                {
                    _engine.PunishRevolutionaries();
                }

                _engine.ApplyRevolutionCrushedEffects();
                hasGovernmentBeenOverthrown = false;
            }
        }

        return hasGovernmentBeenOverthrown;
    }

    /// <summary>
    ///     Determines if the player attempts to escape in a scenario of revolution or war.
    /// </summary>
    /// <returns><c>true</c> if the player has decided to escape; otherwise, <c>false</c>.</returns>
    private bool DoesPlayerAttemptEscape()
    {
        DialogResult dialogResult = _userInterface.DisplayEscapeAttemptDialog();

        if (dialogResult == DialogResult.Yes)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Attempts an escape from a situation of war or revolution either through helicopter or Leftoto.
    /// </summary>
    private void AttemptEscape()
    {
        // In order to escape by helicopter, the player would have to previously purchased it
        if (_engine.HasPlayerPurchasedHelicopter())
        {
            if (_engine.IsPlayerAbleToEscapeByHelicopter())
            {
                _userInterface.DisplayHelicopterEscapeScreen();
            }
            else
            {
                _userInterface.DisplayHelicopterWontStartScreen();
                EscapeToLeftoto();
            }
        }
        else
        {
            EscapeToLeftoto();
        }
    }

    /// <summary>
    ///     Handles the escape of the player to Leftoto and the interaction with the guerrillas.
    /// </summary>
    private void EscapeToLeftoto()
    {
        _userInterface.DisplayEscapeToLeftotoScreen();

        if (_engine.DoesGuerrillaCatchPlayerEscaping())
        {
            _userInterface.DisplayGuerillasCelebratingScreen();
            // TODO: kill player?
        }
        else
        {
            _userInterface.DisplayGuerillasMissedScreen();
        }
    }

    /// <summary>
    ///     Processes the end of the current game by displaying the screen with the final score and possibly saving the high-score.
    /// </summary>
    private void ProcessEnd()
    {
        Score score = _engine.GetCurrentScore();

        _userInterface.DisplayEndScreen(score);
        _engine.SaveHighScore();
    }
}
