using System;
using System.Collections.Generic;
using System.Text;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;

namespace Dictator.ConsoleInterface
{
    public class Game
    {
        private readonly IEngine engine;
        private readonly IUserInterface userInterface;

        public Game(IEngine engine, IUserInterface userInterface)
        {
            this.engine = engine;
            this.userInterface = userInterface;
        }

        public void Start()
        {
            engine.Initialise();
            userInterface.Initialise();
            userInterface.DisplayIntroScreen();
            userInterface.DisplayTitleScreen();
            DisplayWelcomeScreen();
            DisplayAccountDetails();
            userInterface.DisplayPoliceReportScreen();

            while (true)
            {
                engine.SetMonthlyMinimalPopularityAndStrength();
                engine.SetMonthlyRevolutionStrength();

                AdvanceAndDisplayCurrentMonth();

                engine.Plot();
                ProcessTreasuryCosts();
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
                TryTriggerNews();

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

        private void DisplayWelcomeScreen()
        {
            int highscore = engine.GetHighscore();

            userInterface.DisplayWelcomeScreen(highscore);
        }

        private void AdvanceAndDisplayCurrentMonth()
        {
            engine.AdvanceMonth();

            int currentMonth = engine.GetMonth();

            userInterface.DisplayMonthScreen(currentMonth);
        }

        private void ProcessTreasuryCosts()
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

        private void DisplayAccountDetails()
        {
            Account account = engine.GetAccount();

            userInterface.DisplayAccountScreen(account);
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

        private bool DoesPlayerAcceptAdvice()
        {
            DialogResult dialogResult = userInterface.DisplayAdviceRequestDialog();

            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

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

        private void ProcessPoliceReport()
        {
            PoliceReportRequest policeReportRequest = engine.RequestPoliceReport();
            DialogResult dialogResult = userInterface.DisplayPoliceReportRequestDialog(policeReportRequest);

            if (dialogResult == DialogResult.Yes)
            {
                engine.PayFromTreasury(1);
                userInterface.DisplayPoliceReportScreen();
            }
        }

        private bool TryProcessAssassinationAttempt()
        {
            if (engine.ShouldAssassinationAttemptHappen())
            {
                userInterface.DisplayAssassinationAttempt();

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

        private bool TryProcessConflict()
        {
            if (engine.DoesConflictExist())
            {
                if (engine.ShouldWarHappen())
                {
                    userInterface.DisplayWarScreen();

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
                    }

                    return true;
                }
                else
                {
                    userInterface.DisplayWarThreatScreen();
                    engine.ApplyThreatOfWarEffects();
                }
            }

            return false;
        }

        private void ProcessWarWonScenario()
        {
            userInterface.DisplayWarWonScreen();
            engine.SetGroupStrength(GroupType.Leftotans, 0);
        }

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
                    int optionSelected = userInterface.DisplayPresidentialDecisionSubDialog(decisionType);

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
                AskForLoan(Country.America);
                hasDecisionBeenCompleted = true;
            }
            else if (decision.DecisionSubType == DecisionSubType.AskForRussianLoan)
            {
                AskForLoan(Country.Russia);
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
                        if(decision.DecisionSubType == DecisionSubType.PurchaseHelicopter)
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
        private void AskForLoan(Country country)
        {
            userInterface.DisplayLoanApplicationScreen();

            LoanApplicationResult loanApplicationResult = engine.AskForLoan(country);

            userInterface.DisplayLoanApplicationResultScreen(loanApplicationResult);
        }

        /// <summary>
        ///     Attempts to trigger a random unused newsflash.
        /// </summary>
        private void TryTriggerNews()
        {
            if (engine.ShouldNewsHappen())
            {
                if (engine.DoesUnusedNewsExist())
                {
                    Core.News unusedRandomNews = engine.GetRandomUnusedNews();

                    userInterface.DisplayNewsScreen(unusedRandomNews.Text);
                    engine.ApplyNewsEffects(unusedRandomNews);
                    engine.Plot();
                    ProcessPoliceReport();
                }
            }
        }

        private bool TryTriggerRevolution()
        {
            bool shouldRevolutionHappen = engine.TryTriggerRevoltGroup();

            return shouldRevolutionHappen;
        }

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
                // TODO: process the revolution
                engine.InitialiseRevolution();

                // TODO: pass required data and display the ask for help screen
                //userInterface.DisplayRevolutionAskForHelpScreen();

                userInterface.DisplayRevolutionStartedScreen();

                bool temporaryDoesRevolutionFail = true;
                
                if(temporaryDoesRevolutionFail)
                {
                    DialogResult dialogResult = userInterface.DisplayRevolutionCrushedDialog();

                    if(dialogResult == DialogResult.Yes)
                    {
                        engine.PunishRevolutionaries();                        
                    }

                    engine.ApplyRevolutionCrushedEffects();
                    hasGovernmentBeenOverthrown = false;
                }
                else
                {
                    userInterface.DisplayRevolutionOverthrownScreen();
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
        }

        private void EscapeToLeftoto()
        {
            userInterface.DisplayEscapeToLeftotoScreen();

            if (engine.DoesGuerrilaCatchPlayerEscaping())
            {
                userInterface.DisplayGuerillasCelebratingScreen();
                // TODO: kill player?
            }
            else
            {
                userInterface.DisplayGuerillasMissedScreen();
            }
        }

        private void ProcessEnd()
        {
            Score score = engine.GetCurrentScore();

            userInterface.DisplayEndScreen(score);
            engine.SaveHighScore();
        }
    }
}
