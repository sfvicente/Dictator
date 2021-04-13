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

            if (DoesPlayerAcceptAdvice())
            {
                userInterface.DisplayAdviceScreen();
            }

            AskForAudienceDecision();
            userInterface.DisplayAccountScreen();
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
            bool hasChosenOption = true;

            while (hasChosenOption)
            {
                DecisionType decisionType = userInterface.DisplayPresidentialDecisionMainDialog();

                if (decisionType == DecisionType.None)
                {
                    hasChosenOption = false;
                }
                else
                {
                    int optionSelected = userInterface.DisplayPresidentialDecisionSubDialog(decisionType);

                    if (engine.DoesPresidentialOptionExistAndIsAvailable(decisionType, optionSelected))
                    {
                        Decision decision = engine.GetDecisionByTypeAndIndex(decisionType, optionSelected);

                        ExecuteSelectedDecision(decision);
                    }
                }
            }
        }

        private void ExecuteSelectedDecision(Decision decision)
        {
            if (decision.DecisionSubType == DecisionSubType.TransferToSwissAccount)
            {

            }
            else if (decision.DecisionSubType == DecisionSubType.AskForALoan)
            {

            }
            else
            {
                if (DoesPlayerAcceptAdvice())
                {
                    userInterface.DisplayAdviceScreen(decision);
                }

                // TODO: Ask player for confirmation to execute decision

                if (decision.DecisionSubType == DecisionSubType.IncreaseBodyGuard)
                {
                    
                }
                else
                {
                    engine.MarkDecisionAsUsed(decision.Text);
                }

                engine.ApplyDecisionEffects(decision);
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

            // TODO: Pass score to the end screen

            userInterface.DisplayEndScreen();
        }
    }
}
