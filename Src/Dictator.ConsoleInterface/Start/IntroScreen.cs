using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Start
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
            Console.BackgroundColor = ConsoleColor.Black;
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(3, 5, "DICTATOR", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 9, "  Devised and Written by        ", ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 11, "  Don PRIESTLEY                 ", ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 15, "  Copyright  ", ConsoleColor.White);
            ConsoleEx.Write("D", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.Write("k", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.Write("T", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.Write("R", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.Write("O", ConsoleColor.Cyan, ConsoleColor.Black);
            ConsoleEx.Write("N", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.Write("I", ConsoleColor.Cyan, ConsoleColor.Black);
            ConsoleEx.Write("C", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.Write("S", ConsoleColor.Cyan, ConsoleColor.Black);
            ConsoleEx.Write("  1983", ConsoleColor.White);
            ConsoleEx.WriteAt(1, 18, "  Rewritten in C# by ", ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 20, "  Sergio Vicente 2021  ", ConsoleColor.Gray);
            pressAnyKeyControl.Show();
        }
    }
}
