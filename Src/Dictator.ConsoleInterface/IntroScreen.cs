using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class IntroScreen : IIntroScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public IntroScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Black);
            ConsoleEx.WriteAt(2, 4, "DICTATOR", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 8, "  Devised and Written by        ");
            ConsoleEx.WriteAt(1, 10, "  Don PRIESTLEY                 ");
            ConsoleEx.WriteAt(1, 14, "  Copyright  ");
            ConsoleEx.Write("D", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.Write("k", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.Write("T", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.Write("R", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.Write("O", ConsoleColor.Cyan, ConsoleColor.Black);
            ConsoleEx.Write("N", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.Write("I", ConsoleColor.Cyan, ConsoleColor.Black);
            ConsoleEx.Write("C", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.Write("S", ConsoleColor.Cyan, ConsoleColor.Black);
            Console.Write("  1983");
            ConsoleEx.WriteAt(1, 17, "  Rewritten in C# by ");
            ConsoleEx.WriteAt(1, 18, "   Sergio Vicente 2021  ");

            pressAnyKeyControl.Show();
            
            Console.ReadKey(true);
        }
    }
}
