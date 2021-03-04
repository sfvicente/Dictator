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

                // TODO: audience > this.userInterface.DisplayRequestScreen();
                // TODO: plot

                // TODO: if(engine.AssassinationAtempt() or engine.WarHasStarted())
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

                // TODO: if(engine.Revolution())
                if(false)
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
    }
}
