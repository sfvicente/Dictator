using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Common
{
    public class PressAnyKeyOrOptionControl : IPressAnyKeyOrOptionControl
    {
        private readonly IKeyPanel keyPanel;

        public PressAnyKeyOrOptionControl(IKeyPanel keyPanel)
        {
            this.keyPanel = keyPanel;
        }

        public ConsoleKey Show()
        {
            keyPanel.Show();

            return Console.ReadKey(true).Key;
        }
    }
}
