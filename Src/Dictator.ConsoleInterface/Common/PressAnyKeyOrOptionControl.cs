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

        /// <summary>
        ///     Displays the control.
        /// </summary>
        /// <returns>The console key that has was pressed after the control is displayed.</returns>
        public ConsoleKey Show()
        {
            keyPanel.Show();

            return Console.ReadKey(true).Key;
        }
    }
}
