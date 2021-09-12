using System;

namespace Dictator.ConsoleInterface.Common
{
    /// <summary>
    ///     Represents the control that is displayed with a panel when the user is required
    ///     to press a key or select an option.
    /// </summary>
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
