using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Common.Extensions
{
    public static class ConsoleEx
    {
        private const int ScreenRows = 24;
        private const int ScreenCols = 32;
        private const int ScreenColPadding = 24;


        public static void Write(string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            ConsoleColor previousBackgroundColor = Console.BackgroundColor;
            ConsoleColor previousForegroundColor = Console.ForegroundColor;

            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(text);
            Console.BackgroundColor = previousBackgroundColor;
            Console.ForegroundColor = previousForegroundColor;
        }

        public static void WriteAt(int left, int top, string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            ConsoleColor previousBackgroundColor = Console.BackgroundColor;
            ConsoleColor previousForegroundColor = Console.ForegroundColor;

            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            ConsoleEx.WriteAt(left, top, text);
            Console.BackgroundColor = previousBackgroundColor;
            Console.ForegroundColor = previousForegroundColor;
        }

        public static void WriteEmptyLineAt(int top)
        {
            WriteAt(ScreenColPadding, top, "                                ");
        }

        public static void WriteEmptyLineAt(int top, ConsoleColor backgroundColor)
        {
            ConsoleColor previousBackgroundColor = Console.BackgroundColor;
            ConsoleColor previousForegroundColor = Console.ForegroundColor;

            Console.BackgroundColor = backgroundColor;
            ConsoleEx.WriteAt(24, top, "                                ");
            Console.BackgroundColor = previousBackgroundColor;
        }   

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
            
            for(int row = 0; row < ScreenRows; row++)
            {
                WriteEmptyLineAt(row);
            }

            Console.BackgroundColor = previousBackgroundColor;
        }

        public static void Clear(ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;

            for (int row = 0; row < ScreenRows; row++)
            {
                WriteEmptyLineAt(row);
            }
        }
    }
}
