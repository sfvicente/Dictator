using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class IntroScreen : IIntroScreen
    {
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Black);
            ConsoleEx.WriteAt(24, 4, "  DICTATOR                      ");
            ConsoleEx.WriteAt(24, 8, "  Devised and Written by        ");
            ConsoleEx.WriteAt(24, 10, "  Don PRIESTLEY                 ");

            ConsoleEx.WriteAt(24, 14, "  Copyright  ");
            Console.Write('D');
            Console.Write('k');
            Console.Write('T');
            Console.Write('R');
            Console.Write('O');
            Console.Write('N');
            Console.Write('I');
            Console.Write('C');
            Console.Write('S');
            Console.Write("  1983");

            ConsoleEx.WriteAt(24, 18, "  Rewritten in C# by Sergio Vicente 2021  ");
            Console.ReadKey();
        }
    }
}
