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
            userInterface.DisplayAccountScreen();
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
                    userInterface.DisplayEndScreen();
                    break;
                }

                engine.Plot();
                ProcessPoliceReport();
                HandlePresidentialDecision();
                ProcessPoliceReport();
                ProcessNews();
                TryTriggerRevolution();
            }
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
            engine.SetRandomAudienceRequest();
            userInterface.DisplayAudienceScreen();

            if (AcceptsAdvice())
            {
                userInterface.DisplayAdviceScreen();
            }

            AskForAudienceDecision();
            userInterface.DisplayAccountScreen();
        }

        private bool AcceptsAdvice()
        {
            DialogResult dialogResult = userInterface.DisplayAdviceRequestDialog();

            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        private void AskForAudienceDecision()
        {
            DialogResult dialogResult = userInterface.DisplayAudienceDecisionDialog();

            if (dialogResult == DialogResult.Yes)
            {
                engine.AcceptAudienceRequest();
            }
            else
            {
                engine.RefuseAudienceRequest();
            }
        }

        private void ProcessPoliceReport()
        {
            if (userInterface.DisplayPoliceReportRequestDialog())
            {
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

                    //engine.ApplyThreatOfWarEffects();
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
                    //userInterface.DisplayHelicopterEscapeScreen();
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
        }

        private void HandlePresidentialDecision()
        {
            DecisionType decisionType = userInterface.DisplayPresidentialDecisionMainDialog();

            if (decisionType != DecisionType.None)
            {
                int optionSelected = userInterface.DisplayPresidentialDecisionSubDialog(decisionType);

                if (engine.DoesPresidentialOptionExistAndIsAvailable(decisionType, optionSelected))
                {
                    Decision decision = engine.GetDecisionByTypeAndIndex(decisionType, optionSelected);

                    switch(decision.DecisionSubType)
                    {
                        // Handle special cases: IncreaseBodyGuard(), TransferToSwissAccount(), AskForALoan()

                        case DecisionSubType.IncreaseBodyGuard:
                            return;
                        case DecisionSubType.TransferToSwissAccount:
                            return;
                        case DecisionSubType.AskForALoan:
                            return;
                        default:
                            // Otherwise, apply effects of presidential decision
                            return;
                    }                  
                }
            }
        }

        private void AskForLoan(Country country)
        {
            LoanRequest loanRequest = engine.AskForLoan(country);

            //if(loanRequest.IsAccepted)
            //{
            //    userInterface.DisplayLoanScreen();
            //}
            //else
            //{
            //    // TODO: loan rejected?
            //}
        }

        private void ProcessNews()
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

        private void TryTriggerRevolution()
        {
            bool shouldRevolutionHappen = engine.TryTriggerRevoltGroup();

            if (shouldRevolutionHappen)
            {
                userInterface.DisplayRevolutionScreen();

                if (AttemptsEscape())
                {
                    // In order to escape by helicopter, the player would have to previously purchased it
                    if (engine.HasPlayerPurchasedHelicopter())
                    {
                        if (engine.IsPlayerAbleToEscapeByHelicopter())
                        {
                            userInterface.DisplayEscapeByHelicopterScreen();
                        }
                        else
                        {
                            userInterface.DisplayEscapeByHelicopterFailScreen();
                            userInterface.DisplayEscapeToLeftotoScreen();

                            if (engine.DoesGuerrilaCatchPlayerEscaping())
                            {
                                // TODO: guerrila celebration
                                // TODO: kill player?
                            }
                            else
                            {
                                // TODO: guerrila didn't catch player
                            }
                        }
                    }

                    userInterface.DisplayEndScreen();
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
                }
            }
        }

        private bool AttemptsEscape()
        {
            DialogResult dialogResult = userInterface.DisplayEscapeAttemptDialog();

            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }
    }
}
