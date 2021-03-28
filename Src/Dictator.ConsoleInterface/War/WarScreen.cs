using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.War
{
    public class WarScreen : IWarScreen
    {
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Red);
            ConsoleEx.WriteAt(7, 8, " LEFTOTO  INVADES ", ConsoleColor.Black, ConsoleColor.White);
            ConsoleEx.WriteAt(1, 12, "     Ritimban Strength is XX    ", ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 14, "     Leftotan Strength is YY    ", ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleEx.WriteAt(6, 18, "A SHORT DECISIVE WAR", ConsoleColor.White, ConsoleColor.Black);

            // TODO: replace with x seconds delay
            Console.ReadKey(true);
        }
    }
}
