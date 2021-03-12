using Dictator.Common.Extensions;
using System;

namespace Dictator.ConsoleInterface
{
    public class TitleScreen : ITitleScreen
    {
        public void Draw()
        {
            Console.CursorVisible = false;

            ConsoleEx.Clear();
            this.DrawBackground();
            this.DrawContent();
            Console.ReadKey(true);

        }

        public void DrawBackground()
        {
            for (int i = 1; i < 25; i++)
            {
                ConsoleEx.WriteAt(0, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.Cyan);
                ConsoleEx.WriteAt(8, i, "DICTATOR", ConsoleColor.Cyan, ConsoleColor.Black);
                ConsoleEx.WriteAt(16, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.Cyan);
                ConsoleEx.WriteAt(24, i, "DICTATOR", ConsoleColor.Cyan, ConsoleColor.Black);
            }
        }

        public void DrawContent()
        {
            ConsoleEx.WriteAt(0, 3, "Press any key to become DICTATOR", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.WriteAt(12, 5, " of the ", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.WriteAt(12, 8, "RITIMBAN", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(13, 10, "******", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(15, 11, "**", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(13, 12, "******", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(12, 14, "REPUBLIC", ConsoleColor.Green, ConsoleColor.Black);
        }
    }
}
