using System;

namespace Dictator.ConsoleInterface
{
    public class TitleScreen
    {
        public void WriteAt(int left, int top, string text)
        {
            // Validate parameters

            Console.SetCursorPosition(left, top);
            Console.Write(text);
        }

        public void WriteLineAt(int left, int top, string text)
        {
            // Validate parameters

            Console.SetCursorPosition(left, top);
            Console.Write(text);
        }

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
                this.WriteAt(1, i, "DICTATOR");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                this.WriteAt(9, i, "DICTATOR");
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
                this.WriteAt(17, i, "DICTATOR");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                this.WriteAt(25, i, "DICTATOR");
            }
        }

        public void DrawContent()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            this.WriteAt(24, 3, "Press any key to become DICTATOR");
            this.WriteAt(24, 5, "             of the             ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            this.WriteAt(24, 8, "            RITIMBAN            ");
            this.WriteAt(24, 14, "            REPUBLIC            ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            this.WriteAt(24, 10, "             ******             ");
            this.WriteAt(24, 11, "               **               ");
            this.WriteAt(24, 12, "             ******             ");          
        }
    }
}
