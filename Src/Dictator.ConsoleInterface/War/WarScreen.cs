﻿using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.War
{
    public class WarScreen : IWarScreen
    {
        private int ritimbanStrength;
        private int leftotanStrength;

        public WarScreen()
        {
            // TODO: need to get these values into the class through DI
            ritimbanStrength = 0;
            leftotanStrength = 0;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Red);
            ConsoleEx.WriteAt(7, 8, " LEFTOTO  INVADES ", ConsoleColor.Black, ConsoleColor.White);
            ConsoleEx.WriteAt(1, 12, $"     Ritimban Strength is {ritimbanStrength}    ", ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 14, $"     Leftotan Strength is {leftotanStrength}    ", ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleEx.WriteAt(6, 18, "A SHORT DECISIVE WAR", ConsoleColor.White, ConsoleColor.Black);
            Console.ReadKey(true);
        }
    }
}