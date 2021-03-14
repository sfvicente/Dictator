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

        public static void Write(string text, ConsoleColor foregroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.Write(text);
        }

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

        public static void WriteEmptyLineAt(int top)
        {
            WriteAt(1, top, "                                ");
        }

        public static void WriteEmptyLineAt(int top, ConsoleColor backgroundColor)
        {
            ConsoleColor previousBackgroundColor = Console.BackgroundColor;
            ConsoleColor previousForegroundColor = Console.ForegroundColor;

            Console.BackgroundColor = backgroundColor;
            WriteAt(1, top, "                                ");
            Console.BackgroundColor = previousBackgroundColor;
        }

        public static void WriteAt(int left, int top, string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            ConsoleColor previousBackgroundColor = Console.BackgroundColor;
            ConsoleColor previousForegroundColor = Console.ForegroundColor;

            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            WriteAt(left, top, text);
            Console.BackgroundColor = previousBackgroundColor;
            Console.ForegroundColor = previousForegroundColor;
        }

        public static void WriteAt(int left, int top, string text, ConsoleColor foregroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            WriteAt(left, top, text);
        }

        public static void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(ScreenColPadding + left - 1, top - 1);
        }

        public static void WriteAt(int left, int top, string text)
        {
            // Validate parameters
            
            SetCursorPosition(left, top);
            Console.Write(text);
        }

        public static void Clear()
        {
            for (int row = 1; row <= ScreenRows; row++)
            {
                WriteEmptyLineAt(row);
            }
        }

        public static void Clear(char character)
        {
            for (int row = 1; row <= ScreenRows; row++)
            {
                for (int col = 1; col <= ScreenCols; col++)
                {
                    WriteAt(col, row, character.ToString());
                }
            }
        }

        public static void Clear(ConsoleColor backgroundColor)
        {
            ConsoleColor previousBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Clear();
            Console.BackgroundColor = previousBackgroundColor;
        }

        public static void Clear(ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Clear();
        }

        public static void Clear(char character, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Clear(character);
        }
    }
}
