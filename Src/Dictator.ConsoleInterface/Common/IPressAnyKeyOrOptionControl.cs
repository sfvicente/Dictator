using System;

namespace Dictator.ConsoleInterface.Common
{
    public interface IPressAnyKeyOrOptionControl
    {
        /// <summary>
        ///     Displays the control.
        /// </summary>
        /// <returns>The console key that has was pressed after the control is displayed.</returns>
        public ConsoleKey Show();
    }
}
