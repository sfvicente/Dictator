using Dictator.Common.Extensions;
using System;

namespace Dictator.ConsoleInterface
{
    public class TitleScreen : ITitleScreen
    {
        public void Draw()
        {
            Console.CursorVisible = false;

            Console.Clear();
            //this.DrawBackground();
            this.DrawContent();
            Console.Read();

        }

        public void DrawBackground()
        {
            for (int i = 1; i < 25; i++)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
                ConsoleEx.WriteAt(1, i, "DICTATOR");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                ConsoleEx.WriteAt(9, i, "DICTATOR");
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
                ConsoleEx.WriteAt(17, i, "DICTATOR");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                ConsoleEx.WriteAt(25, i, "DICTATOR");
            }
        }

        public void DrawContent()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            ConsoleEx.WriteAt(24, 3, "Press any key to become DICTATOR");
            ConsoleEx.WriteAt(24, 5, "             of the             ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleEx.WriteAt(24, 8, "            RITIMBAN            ");
            ConsoleEx.WriteAt(24, 14, "            REPUBLIC            ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            ConsoleEx.WriteAt(24, 10, "             ******             ");
            ConsoleEx.WriteAt(24, 11, "               **               ");
            ConsoleEx.WriteAt(24, 12, "             ******             ");          
        }
    }
}
