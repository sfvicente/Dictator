using System;
using System.Collections.Generic;
using System.Text;

using Dictator.Core;

namespace Dictator.ConsoleInterface
{
    public class Game
    {
        private IEngine engine;
        private UserInterface userInterface;

        public Game(IEngine engine)
        {
            this.engine = engine;
            this.userInterface = new UserInterface();
        }

        public void Start()
        {

            this.userInterface.DisplayTitleScreen();

        }

    }
}
