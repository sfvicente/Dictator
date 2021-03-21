using System;
using System.Collections.Generic;
using System.Text;

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

                ProcessAudience();
                engine.Plot();

                // TODO: if(engine.AssassinationAtempt() or engine.WarHasStarted())
                if (TryProcessAssassinationAttempt())
                {
                    userInterface.DisplayEndScreen();
                    break;
                }

                engine.Plot();
                ProcessPoliceReport();
                HandlePresidentialDecision();
                ProcessPoliceReport();
                ProcessNews();

                if(TryTriggerRevolution())
                {
                    if(AttemptsEscape())
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
                        break;
                    }

                    // TODO: process the revolution
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

        public void ProcessAudience()
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

        public bool AcceptsAdvice()
        {
            DialogResult dialogResult = userInterface.DisplayAdviceRequestDialog();

            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        public void AskForAudienceDecision()
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

        public void ProcessPoliceReport()
        {
            if (userInterface.DisplayPoliceReportRequestDialog())
            {
                userInterface.DisplayPoliceReportScreen();
            }
        }

        public bool TryProcessAssassinationAttempt()
        {
            if(engine.ShouldAssassinationAttemptHappen())
            {
                userInterface.DisplayAssassinationAttempt();

                if(engine.IsAssassinationSuccessful())
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

        public void HandlePresidentialDecision()
        {
            DecisionType decisionType = userInterface.DisplayPresidentialDecisionMainDialog();

            if(decisionType != DecisionType.None)
            {
                userInterface.DisplayPresidentialDecisionSubDialog(decisionType);
            }
        }

        public void ProcessNews()
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

        public bool TryTriggerRevolution()
        {
            if(engine.TryTriggerRevoltGroup())
            {
                // TODO: perform the rest of the actions in the revolution mode





                
            }

            return false;
        }

        public bool AttemptsEscape()
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
