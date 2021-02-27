using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Common.Extensions
{
    public static class ConsoleEx
    {
        public static void WriteAt(int left, int top, string text)
        {
            // Validate parameters

            Console.SetCursorPosition(left, top);
            Console.Write(text);
        }

        public static void WriteLineAt(int left, int top, string text)
        {
            // Validate parameters

            Console.SetCursorPosition(left, top);
            Console.Write(text);
        }

        public static void Clear(ConsoleColor backgroundColor)
        {
            ConsoleColor previousBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Clear();
            Console.BackgroundColor = previousBackgroundColor;
        }
    }
}
