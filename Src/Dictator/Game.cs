using System.Collections.Generic;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;

namespace Dictator.ConsoleInterface
{
    public class Game
    {
        private readonly IEngine engine;
        private readonly IUserInterface userInterface;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game"/> class from an <see cref="IEngine"/> and a <see cref="IUserInterface"/> components.
        /// </summary>
        /// <param name="engine">The engine component responsible for the coordination of business calls to the services.</param>
        /// <param name="userInterface">The user interface component responsible for displaying the screens.</param>
        public Game(IEngine engine, IUserInterface userInterface)
        {
            this.engine = engine;
            this.userInterface = userInterface;
        }

        /// <summary>
        ///     Starts a game cycle which initialises the interface and processes the game flow.
        /// </summary>
        public void Start()
        {
            userInterface.Initialise();

            while (true)
            {
                engine.Initialise();
                userInterface.DisplayIntroScreen();
                userInterface.DisplayTitleScreen();
                DisplayWelcomeScreen();
                DisplayAccountDetails();
                DisplayPoliceReportScreen();

                while (true)
                {
                    engine.SetMonthlyMinimalPopularityAndStrength();
                    engine.SetMonthlyRevolutionStrength();
                    AdvanceAndDisplayCurrentMonth();
                    engine.Plot();
                    ProcessTreasuryMonthlyCosts();
                    HandleAudienceRequest();
                    engine.Plot();

                    if (TryProcessAssassinationAttempt() || TryProcessConflict())
                    {
                        ProcessEnd();
                        break;
                    }

                    engine.Plot();
                    ProcessPoliceReport();
                    HandlePresidentialDecision();
                    ProcessPoliceReport();
                    
                    if(TryTriggerNews())
                    {
                        ExecuteRandomNewsflash();
                        engine.Plot();
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
            int highscore = engine.GetHighScore();

            userInterface.DisplayWelcomeScreen(highscore);
        }

        /// <summary>
        ///     Advances the current month and displays it on the month screen.
        /// </summary>
        private void AdvanceAndDisplayCurrentMonth()
        {
            engine.AdvanceMonth();

            int currentMonth = engine.GetMonth();

            userInterface.DisplayMonthScreen(currentMonth);
        }

        /// <summary>
        ///     Handles the payment of the monthly costs and the check for bankruptcy.
        /// </summary>
        private void ProcessTreasuryMonthlyCosts()
        {
            engine.PayMonthlyCosts();

            if (engine.IsGovernmentBankrupt())
            {
                userInterface.DisplayBankuptcyScreen();
                engine.ApplyBankruptcyEffects();
                engine.Plot();  // Realculate probability for the assassination and revolution events
                ProcessPoliceReport();
            }
        }

        /// <summary>
        ///     Displays the account details screen.
        /// </summary>
        private void DisplayAccountDetails()
        {
            Account account = engine.GetAccount();

            userInterface.DisplayAccountScreen(account);
        }

        /// <summary>
        ///     Displays the police report screen.
        /// </summary>
        private void DisplayPoliceReportScreen()
        {
            PoliceReport policeReport = engine.GetPoliceReport();

            userInterface.DisplayPoliceReportScreen(policeReport);
        }

        /// <summary>
        ///     Handles the generation and processing of an audience request from one of the groups.
        /// </summary>
        private void HandleAudienceRequest()
        {
            Core.Audience audience = engine.SelectRandomUnusedAudienceRequest();

            userInterface.DisplayAudienceScreen(audience);

            if (DoesPlayerAcceptAdvice())
            {
                userInterface.DisplayAdviceScreen(audience);
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
            DialogResult dialogResult = userInterface.DisplayAdviceRequestDialog();

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
        private void AskForAudienceDecision(Core.Audience audience)
        {
            DialogResult dialogResult = userInterface.DisplayAudienceDecisionDialog(audience);

            if (dialogResult == DialogResult.Yes)
            {
                engine.AcceptAudienceRequest(audience);
            }
            else
            {
                engine.RefuseAudienceRequest(audience);
            }
        }

        /// <summary>
        ///     Asks the player if the police report should be displayed and if the user accepts, displays it.
        /// </summary>
        private void ProcessPoliceReport()
        {
            PoliceReportRequest policeReportRequest = engine.RequestPoliceReport();
            DialogResult dialogResult = userInterface.DisplayPoliceReportRequestDialog(policeReportRequest);

            if (dialogResult == DialogResult.Yes)
            {
                engine.PayFromTreasury(1);
                DisplayPoliceReportScreen();
            }
        }

        /// <summary>
        ///     Tries to process an assassination attempt, determining if it should happen and then if it is successful.
        /// </summary>
        /// <returns><c>true</c> if an assassination is processed and successful that leads to the end of the current game; otherwise, <c>false</c>.</returns>
        private bool TryProcessAssassinationAttempt()
        {
            if (engine.ShouldAssassinationAttemptHappen())
            {
                string groupName = engine.GetAssassinationGroupName();

                userInterface.DisplayAssassinationAttempt(groupName);

                if (engine.IsAssassinationSuccessful())
                {
                    // As attempt was successful, display death screen and kill player as game must end
                    userInterface.DisplayAssassinationSuccededScreen();
                    engine.KillPlayer();
                    return true;
                }

                userInterface.DisplayAssassinationFailedScreen();
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

            if (engine.DoesConflictExist())
            {
                if (engine.ShouldWarHappen())
                {
                    WarStats warStats = engine.BeginInvasion();
             
                    userInterface.DisplayLeftotoInvadesScreen(warStats);

                    bool hasWarBeenWon = engine.ExecuteWar(warStats);

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
                    userInterface.DisplayWarThreatScreen();
                    engine.ApplyThreatOfWarEffects();
                }
            }

            return isGameOver;
        }

        /// <summary>
        ///     Processes the scenario when the player wins the war.
        /// </summary>
        private void ProcessWarWonScenario()
        {
            userInterface.DisplayWarWonScreen();
            engine.SetGroupStrength(GroupType.Leftotans, 0);
        }

        /// <summary>
        ///     Processes the scenario when the player loses the war.
        /// </summary>
        private void ProcessWarLostScenario()
        {
            userInterface.DisplayWarLostScreen();

            if (engine.HasPlayerPurchasedHelicopter())
            {
                if (engine.IsPlayerAbleToEscapeAfterLosingWar())
                {
                    userInterface.DisplayHelicopterEscapeScreen();
                }
                else
                {
                    userInterface.DisplayHelicopterEngineFailure();
                    engine.KillPlayer();
                }
            }
            else
            {
                userInterface.DisplayWarExecutionScreen();
                engine.KillPlayer();
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
                DecisionType decisionType = userInterface.DisplayPresidentialDecisionMainDialog();

                if (decisionType == DecisionType.None)
                {
                    hasNotChosenDecisionOrExited = false;
                }
                else
                {
                    Decision[] decisions = engine.GetDecisionsByType(decisionType);
                    int optionSelected = userInterface.DisplayPresidentialDecisionSubDialog(decisions);

                    if (engine.DoesPresidentialOptionExistAndIsAvailable(decisionType, optionSelected))
                    {
                        Decision decision = engine.GetDecisionByTypeAndIndex(decisionType, optionSelected);

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
                SwissBankAccountTransfer transfer = engine.TransferToSwissBankAccount();
                Account account = engine.GetAccount();

                userInterface.DisplayTransferToSwissBankAccount(transfer, account);
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
                    userInterface.DisplayAdviceScreen(decision);
                }

                if (DoesPlayerConfirmDecision(decision))
                {
                    if (decision.DecisionSubType == DecisionSubType.IncreaseBodyGuard)
                    {
                        engine.IncreaseBodyguard();
                    }
                    else
                    {
                        if (decision.DecisionSubType == DecisionSubType.PurchaseHelicopter)
                        {
                            engine.PurchasedHelicopter();
                        }

                        engine.MarkDecisionAsUsed(decision.Text);
                    }

                    engine.ApplyDecisionEffects(decision);
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
            DialogResult dialogResult = userInterface.DisplayPresidentialDecisionActionDialog(decision);

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
            userInterface.DisplayLoanApplicationScreen();

            LoanApplicationResult loanApplicationResult = engine.AskForLoan(country);

            userInterface.DisplayLoanApplicationResultScreen(loanApplicationResult);
        }

        /// <summary>
        ///     Attempts to trigger a random unused newsflash.
        /// </summary>
        /// <returns><c>true</c> if the news should happen; otherwise, <c>false</c>.</returns>
        private bool TryTriggerNews()
        {
            if (engine.ShouldNewsHappen())
            {
                if (engine.DoesUnusedNewsExist())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Executes a random unused newsflash.
        /// </summary>
        private void ExecuteRandomNewsflash()
        {
            Core.News unusedRandomNews = engine.SelectRandomUnusedNews();

            userInterface.DisplayNewsScreen(unusedRandomNews.Text);
            engine.ApplyNewsEffects(unusedRandomNews);
        }

        /// <summary>
        ///     Attempts to create a revolution scenario by setting one of the groups as a revolutionary element.
        /// </summary>
        /// <returns><c>true</c> if a revolution is triggered; otherwise, <c>false</c>.</returns>
        private bool TryTriggerRevolution()
        {
            bool shouldRevolutionHappen = engine.TryTriggerRevoltGroup();

            return shouldRevolutionHappen;
        }

        /// <summary>
        ///     Initiates the revolution and attempts to overthrown the government.
        /// </summary>
        /// <returns><c>true</c> if the revolution succeeded and the government has been overthrown; otherwise, <c>false</c>.</returns>
        private bool TryProcessGovernmentOverthrown()
        {
            userInterface.DisplayRevolutionScreen();

            bool hasGovernmentBeenOverthrown = true;

            if (DoesPlayerAttemptEscape())
            {
                AttemptEscape();
            }
            else
            {
                Dictionary<int, Group> possibleAllies = engine.FindPossibleAlliesForPlayer();

                if (possibleAllies.Count > 0)
                {
                    int selectedAllyGroupId = userInterface.DisplayRevolutionAskForHelpDialog(possibleAllies);

                    if(engine.DoesGroupAcceptAllianceInRevolution(selectedAllyGroupId))
                    {
                        engine.SetPlayerAllyForRevolution(selectedAllyGroupId);
                    }
                    else
                    {
                        userInterface.DisplayRevolutionAllyLowPopularityScreen();
                    }
                }
                else
                {
                    userInterface.DisplayRevolutionNoAlliesScreen();
                }

                userInterface.DisplayRevolutionStartedScreen();

                bool doesRevolutionSucceed = engine.DoesRevolutionSucceed();

                if (doesRevolutionSucceed)
                {
                    userInterface.DisplayRevolutionOverthrownScreen();
                    engine.KillPlayer();
                }
                else
                {
                    DialogResult dialogResult = userInterface.DisplayRevolutionCrushedDialog();

                    if (dialogResult == DialogResult.Yes)
                    {
                        engine.PunishRevolutionaries();
                    }

                    engine.ApplyRevolutionCrushedEffects();
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
            DialogResult dialogResult = userInterface.DisplayEscapeAttemptDialog();

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
            if (engine.HasPlayerPurchasedHelicopter())
            {
                if (engine.IsPlayerAbleToEscapeByHelicopter())
                {
                    userInterface.DisplayHelicopterEscapeScreen();
                }
                else
                {
                    userInterface.DisplayHelicopterWontStartScreen();
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
            userInterface.DisplayEscapeToLeftotoScreen();

            if (engine.DoesGuerrillaCatchPlayerEscaping())
            {
                userInterface.DisplayGuerillasCelebratingScreen();
                // TODO: kill player?
            }
            else
            {
                userInterface.DisplayGuerillasMissedScreen();
            }
        }

        /// <summary>
        ///     Processes the end of the current game by displaying the screen with the final score and possibly saving the high-score.
        /// </summary>
        private void ProcessEnd()
        {
            Score score = engine.GetCurrentScore();

            userInterface.DisplayEndScreen(score);
            engine.SaveHighScore();
        }
    }
}
