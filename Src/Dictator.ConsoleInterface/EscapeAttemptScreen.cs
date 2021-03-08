using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class EscapeAttemptScreen : IEscapeAttemptScreen
    {
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(24, 12, "        ESCAPE ATTEMPT ?        ");
            
            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            if(keyPressed == ConsoleKey.Y)
            {
                // TODO: process response
            }
        }
    }
}
