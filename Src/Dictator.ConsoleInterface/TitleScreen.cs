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
            this.DrawBackground();
            this.DrawContent();
            Console.ReadKey(true);

        }

        public void DrawBackground()
        {
            for (int i = 1; i < 25; i++)
            {
                ConsoleEx.WriteAt(24, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.Cyan);
                ConsoleEx.WriteAt(32, i, "DICTATOR", ConsoleColor.Cyan, ConsoleColor.Black);
                ConsoleEx.WriteAt(40, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.Cyan);
                ConsoleEx.WriteAt(48, i, "DICTATOR", ConsoleColor.Cyan, ConsoleColor.Black);
            }
        }

        public void DrawContent()
        {
            ConsoleEx.WriteAt(24, 3, "Press any key to become DICTATOR", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.WriteAt(36, 5, " of the ", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.WriteAt(36, 8, "RITIMBAN", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(37, 10, "******", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(39, 11, "**", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(37, 12, "******", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(36, 14, "REPUBLIC", ConsoleColor.Green, ConsoleColor.Black);
        }
    }
}
