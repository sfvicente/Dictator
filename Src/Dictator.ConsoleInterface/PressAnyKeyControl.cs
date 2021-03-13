using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PressAnyKeyControl : IPressAnyKeyControl
    {
        public void Show()
        {
            ConsoleEx.WriteAt(1, 22, "                                ", ConsoleColor.Blue, ConsoleColor.White);
            ConsoleEx.WriteAt(1, 23, "              KEY               ", ConsoleColor.Blue, ConsoleColor.White);
            ConsoleEx.WriteAt(1, 24, "                                ", ConsoleColor.Blue, ConsoleColor.White);
        }
    }
}
