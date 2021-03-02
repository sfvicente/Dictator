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
            this.userInterface.DisplayIntroScreen();
            this.userInterface.DisplayTitleScreen();
            this.userInterface.DisplayWelcomeScreen();
            this.userInterface.DisplayAccountScreen();
            userInterface.DisplayPoliceReportScreen();

            while (true)
            {
                engine.SetMonthlyMinimalPopularityAndStrength();
                engine.SetMonthlyRevolutionStrength();
                this.engine.AdvanceMonth();
                userInterface.DisplayMonthScreen();

                // TODO: engine.Plot()
                if (engine.IsGovernmentBankrupt())
                {
                    this.userInterface.DisplayBankuptcyScreen();
                    // TODO: engine.Plot()
                    //police report
                }

                // TODO: audience > this.userInterface.DisplayRequestScreen();
                // TODO: plot

                // TODO: if(engine.AssassinationAtempt() or engine.WarHasStarted())
                // TODO: engine.End(); break;

                // TODO: plot
                ProcessPoliceReport();


                // TODO: decision
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
                // TODO: engine.End(); break;

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
