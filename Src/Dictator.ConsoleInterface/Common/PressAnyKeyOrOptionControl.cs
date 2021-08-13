using System;

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
