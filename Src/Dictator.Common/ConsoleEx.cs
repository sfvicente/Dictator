using System;

namespace Dictator.Common
{
    public static class ConsoleEx
    {
        private const int ScreenRows = 24;
        private const int ScreenCols = 32;
        private const int ScreenColPadding = 24;
        private const string EmptyLine = "                                ";

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

        /// <summary>
        ///     Writes an empty line of text at a specific row.
        /// </summary>
        /// <param name="top">The row position of the cursor. Rows are numbered from top to bottom starting at 0.</param>
        public static void WriteEmptyLineAt(int top)
        {
            WriteAt(1, top, EmptyLine);
        }

        /// <summary>
        ///     Writes an empty line of text at a specific row and with a specific background color.
        /// </summary>
        /// <param name="top">The row position of the cursor. Rows are numbered from top to bottom starting at 0.</param>
        /// <param name="backgroundColor">The background color of the line.</param>
        public static void WriteEmptyLineAt(int top, ConsoleColor backgroundColor)
        {
            ConsoleColor previousBackgroundColor = Console.BackgroundColor;

            Console.BackgroundColor = backgroundColor;
            WriteAt(1, top, EmptyLine);
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

        /// <summary>
        ///     Sets the position of the cursor in the console screen.
        /// </summary>
        /// <param name="left">The column position of the cursor. Columns are numbered from left to right starting at 0.</param>
        /// <param name="top">The row position of the cursor. Rows are numbered from top to bottom starting at 0.</param>
        public static void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(ScreenColPadding + left - 1, top - 1);
        }

        /// <summary>
        ///     Writes the text representation of the specified value or values to the screen at the specified position.
        /// </summary>
        /// <param name="left">The column position of the cursor. Columns are numbered from left to right starting at 0.</param>
        /// <param name="top">The row position of the cursor. Rows are numbered from top to bottom starting at 0.</param>
        /// <param name="text">The text to be written to the screen.</param>
        public static void WriteAt(int left, int top, string text)
        {
            // Validate parameters
            
            SetCursorPosition(left, top);
            Console.Write(text);
        }

        public static void WriteCenteredAt(int top, string text)
        {
            int left = (ScreenCols - text.Length) / 2;

            WriteAt(left, top, text);
        }

        /// <summary>
        ///     Clears the screen.
        /// </summary>
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
