using System;
using System.Collections.Generic;
using System.Text;

using Dictator.Core;

namespace Dictator.ConsoleInterface
{
    public class Game
    {
        private Engine engine;
        private UserInterface userInterface;

        public Game()
        {
            this.engine = new Engine();
            this.userInterface = new UserInterface();
        }

        public void Start()
        {

            this.userInterface.DisplayTitleScreen();

        }

    }
}
