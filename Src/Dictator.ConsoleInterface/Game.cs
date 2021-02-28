﻿using System;
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

            while (true)
            {
                this.engine.AdvanceMonth();

                if (engine.IsGovernmentBankrupt())
                {
                    this.userInterface.DisplayBankuptcyScreen();
                    //plot
                    //police report
                }

                this.userInterface.DisplayAccountScreen();
                this.userInterface.DisplayPoliceReportRequestScreen();
                this.userInterface.DisplayPoliceReportScreen();

                this.userInterface.DisplayRequestScreen();
            }
        }

    }
}
