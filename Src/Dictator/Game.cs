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
                userInterface.DisplayAdviceScreen();
                // TODO: plot

                // TODO: if(engine.AssassinationAtempt() or engine.WarHasStarted())
                ProcessAssassinationAttempt();
                if (false)
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
                    userInterface.DisplayEndScreen();
                    break;
                }
            }
        }

        public void ProcessPoliceReport()
        {
            if (userInterface.DisplayPoliceReportRequestDialog())
            {
                userInterface.DisplayPoliceReportScreen();
            }
        }

        public void ProcessAssassinationAttempt()
        {
            if(engine.ShouldAssassinationAttemptHappen())
            {
                userInterface.DisplayAssassinationAttempt();

                // TODO: add check for result
            }
        }

        public bool TryTriggerRevolution()
        {
            if(engine.TryTriggerRevoltGroup())
            {
                // TODO: perform the rest of the actions in the revolution mode


                // In order to escape by helicopter, the player had to previously purchased it
                if (engine.HasPlayerPurchasedHelicopter())
                {
                    if(engine.AttemptEscapeByHelicopter())
                    {
                        userInterface.DisplayEscapeByHelicopterScreen();
                    }
                    else
                    {
                        userInterface.DisplayEscapeByHelicopterFailScreen();
                        return false;
                    }
                }


                
            }

            return true;
        }
    }
}
