using Dictator.Common;
using System;

namespace Dictator.ConsoleInterface.Common
{
    /// <summary>
    ///     Represents the panel that is displayed when the user is required to press a key.
    /// </summary>
    public class KeyPanel : IKeyPanel
    {
        /// <summary>
        ///     Displays the panel.
        /// </summary>
        public void Show()
        {
            ConsoleEx.WriteAt(1, 22, "                                ", ConsoleColor.Blue, ConsoleColor.White);
            ConsoleEx.WriteAt(1, 23, "              KEY               ", ConsoleColor.Blue, ConsoleColor.White);
            ConsoleEx.WriteAt(1, 24, "                                ", ConsoleColor.Blue, ConsoleColor.White);
        }
    }
}
