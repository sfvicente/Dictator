﻿using System;
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
                    //userInterface.DisplayWarThreatScreen();

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
                //Chances of escape are 2/3
                if (true)//engine.HasPlayerEscaped)
                {
                    //userInterface.DisplayHelicopterEscapeScreen();
                }
                else
                {
                    //userInterface.DisplayHelicopterEngineFailure();

                    // engine.KillPlayer();
                }
            }
            else
            {
                //userInterface.DisplayWarExecution();
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
                    //decisionSubType = engine.GetDecisionSubType(decisionType, optionSelected);

                    // Handle special cases: IncreaseBodyGuard(), TransferToSwissAccount(), AskForALoan()
                    // Otherwise, apply effects of presidential decision
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
                if (engine.TryTriggerRandomUnusedNews())
                {
                    userInterface.DisplayNewsScreen();
                    engine.ApplyNewsEffects();
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
                        if (engine.AttemptEscapeByHelicopter())
                        {
                            userInterface.DisplayEscapeByHelicopterScreen();
                        }
                        else
                        {
                            userInterface.DisplayEscapeByHelicopterFailScreen();
                            userInterface.DisplayEscapeToLeftotoScreen();
                            //TODO: add guerrilas interaction
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
