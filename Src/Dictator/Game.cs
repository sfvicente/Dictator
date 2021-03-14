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

                // TODO: engine.Plot()
                if (engine.IsGovernmentBankrupt())
                {
                    userInterface.DisplayBankuptcyScreen();
                    // TODO: engine.Plot()
                    ProcessPoliceReport();
                }

                userInterface.DisplayAudienceScreen();
                if(AcceptsAdvice())
                {
                    userInterface.DisplayAdviceScreen();
                }
                
                // TODO: plot

                // TODO: if(engine.AssassinationAtempt() or engine.WarHasStarted())
                if (TryProcessAssassinationAttempt())
                {
                    userInterface.DisplayEndScreen();
                    break;
                }

                // TODO: plot
                ProcessPoliceReport();
                userInterface.DisplayMainDecisionDialog();
                ProcessPoliceReport();

                if (engine.ShouldNewsHappen())
                {
                    if(engine.TryTriggerRandomUnusedNews())
                    {
                        userInterface.DisplayNewsScreen();
                        // TODO: ApplyNewsEffects
                        // TODO: plot
                        ProcessPoliceReport();
                    }
                }

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

        public bool AcceptsAdvice()
        {
            DialogResult dialogResult = userInterface.DisplayAdviceRequestDialog();

            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }

            return false;
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
                    // TODO: display death screen, game must end
                    return true;
                }

                // TODO: display attempt failed screen
            }

            // Either no attempt happened or it failed, so the game should progress
            return false;
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
