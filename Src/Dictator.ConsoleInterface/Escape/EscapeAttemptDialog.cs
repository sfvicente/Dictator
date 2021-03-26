﻿using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Escape
{
    public class EscapeAttemptDialog : IEscapeAttemptDialog
    {
        public DialogResult Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 12, "        ESCAPE ATTEMPT ?        ");
            
            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            if(keyPressed == ConsoleKey.Y)
            {
                return DialogResult.Yes;
            }

            return DialogResult.No;
        }
    }
}