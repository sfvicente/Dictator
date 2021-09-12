using System;

namespace Dictator.ConsoleInterface.Common
{
    /// <summary>
    ///     Represents the control that is displayed with a panel when the user is required
    ///     to press a key.
    /// </summary>
    public class PressAnyKeyControl : IPressAnyKeyControl
    {
        private readonly IKeyPanel keyPanel;

        public PressAnyKeyControl(IKeyPanel keyPanel)
        {
            this.keyPanel = keyPanel;
        }

        /// <summary>
        ///     Displays the control.
        /// </summary>
        public void Show()
        {
            keyPanel.Show();
            Console.ReadKey(true);
        }
    }
}
