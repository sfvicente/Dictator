using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the screen that is displayed when a revolution has started.
    /// </summary>
    public class RevolutionStartedScreen : IRevolutionStartedScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RevolutionStartedScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public RevolutionStartedScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "   The REVOLUTION has STARTED   ");
            pressAnyKeyControl.Show();
        }
    }
}
