using Dictator.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Common
{
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
