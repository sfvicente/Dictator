using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape
{
    public interface IEscapeToLeftotoScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show();
    }

    /// <summary>
    ///     Represents the screen that is displayed when the player attempts the escape
    ///     through the mountains to Leftoto.
    /// </summary>
    public class EscapeToLeftotoScreen : IEscapeToLeftotoScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EscapeToLeftotoScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public EscapeToLeftotoScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 10, "   You have to get through the  ");
            ConsoleEx.WriteAt(1, 12, "      MOUNTAINS to LEFTOTO      ");
            pressAnyKeyControl.Show();
        }
    }
}
