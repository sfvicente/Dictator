using System;
using System.Collections.Generic;
using System.Text;

using Dictator.Core;

namespace Dictator.ConsoleInterface
{
    public class Game
    {
        private Engine engine;

        public Game()
        {
            this.engine = new Engine();
        }

        public void Start()
        {
            TitleScreen screen = new TitleScreen();

            screen.Draw();
        }

    }
}
