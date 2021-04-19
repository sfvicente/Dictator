using System;
using System.Collections.Generic;
using System.Text;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;

namespace Dictator.ConsoleInterface
{
    public class Game
    {
        private IEngine engine;
        private IUserInterface userInterface;

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
            userInterface.DisplayWelcomeScreen();
            DisplayAccountDetails();
            userInterface.DisplayPoliceReportScreen();

            while (true)
            {
                engine.SetMonthlyMinimalPopularityAndStrength();
                engine.SetMonthlyRevolutionStrength();
                engine.AdvanceMonth();
                userInterface.DisplayMonthScreen();

                engine.Plot();
                CheckAndHandleBankruptcy();

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
                TryProcessNews();

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

        private void DisplayAccountDetails()
        {
            Account account = engine.GetAccount();

            userInterface.DisplayAccountScreen(account);
        }

        private void CheckAndHandleBankruptcy()
        {
            if (engine.IsGovernmentBankrupt())
            {
                userInterface.DisplayBankuptcyScreen();
                engine.ApplyBankruptcyEffects();
                engine.Plot();  // Realculate probability for the assassination and revolution events
                ProcessPoliceReport();
            }
        }

        /// <summary>
        ///     Handles the generation and processing of an audience request from one of the groups.
        /// </summary>
        private void HandleAudienceRequest()
        {
            Core.Audience audience = engine.GetRandomUnusedAudienceRequest();

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
            DialogResult dialogResult = userInterface.DisplayPoliceReportRequestDialog();

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

                    //engine.GenerateWarResult()

                    //TODO: complete logic for outcome

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

        private void ProcessWarWon()
        {
            userInterface.DisplayWarWonScreen();
            engine.SetGroupStrength(GroupType.Leftotans, 0);
        }

        private void ProcessWarLost()
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

                        if(TryProcessSelectedDecision(decision))
                        {
                            hasNotChosenDecisionOrExited = false;
                        }
                    }
                }
            }
        }

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
            else if (decision.DecisionSubType == DecisionSubType.AskForALoan)
            {
                hasDecisionBeenCompleted = true;
            }
            else
            {
                if (DoesPlayerAcceptAdvice())
                {
                    userInterface.DisplayAdviceScreen(decision);
                }

                if(DoesPlayerConfirmDecision(decision))
                {
                    if (decision.DecisionSubType == DecisionSubType.IncreaseBodyGuard)
                    {
                        engine.IncreaseBodyguard();
                    }
                    else
                    {
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

            if(dialogResult == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        private void AskForLoan(Country country)
        {
            LoanApplicationResult loanApplicationResult = engine.AskForLoan(country);

            userInterface.DisplayLoanApplicationResultScreen(loanApplicationResult);
        }

        private void TryProcessNews()
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

            if (DoesPlayerAttemptEscape())
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
                        userInterface.DisplayEscapeToLeftotoScreen();

                        if (engine.DoesGuerrilaCatchPlayerEscaping())
                        {
                            // TODO: guerrila celebration
                            // TODO: kill player?
                        }
                        else
                        {
                            userInterface.DisplayGuerillasMissedScreen();
                        }
                    }
                }

                return true;
            }
            else
            {
                // TODO: process the revolution
                engine.InitialiseRevolution();

                // TODO: pass required data and display the ask for help screen
                //userInterface.DisplayRevolutionAskForHelpScreen();

                // TODO: Display revolution has started screen
                //userInterface.DisplayRevolutionStartedScreen();

                // TODO: Display outcome of the revolution

                // TODO: if revolution is lost return true, if won, return false
                return false;
            }
        }

        private bool DoesPlayerAttemptEscape()
        {
            DialogResult dialogResult = userInterface.DisplayEscapeAttemptDialog();

            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        private void ProcessEnd()
        {
            Score score = engine.GetCurrentScore();

            userInterface.DisplayEndScreen(score);
        }
    }
}
