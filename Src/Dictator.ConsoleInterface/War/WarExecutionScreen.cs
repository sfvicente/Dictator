using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when a player loses the war, is unable to
    ///     escape by helicopter and is executed.
    /// </summary>
    public class WarExecutionScreen : IWarExecutionScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WarExecutionScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public WarExecutionScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 11, "     YOU are judged to be an    ");
            ConsoleEx.WriteAt(1, 13, "   ENEMY of the PEOPLE and ...  ");
            ConsoleEx.WriteAt(1, 15, "       Summarily EXECUTED       ");
            pressAnyKeyControl.Show();
        }
    }
}
