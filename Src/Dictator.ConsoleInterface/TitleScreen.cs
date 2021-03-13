using Dictator.Common.Extensions;
using System;

namespace Dictator.ConsoleInterface
{
    public class TitleScreen : ITitleScreen
    {
        public void Draw()
        {
            ConsoleEx.Clear();
            this.DrawBackground();
            this.DrawContent();
            Console.ReadKey(true);

        }

        public void DrawBackground()
        {
            for (int i = 1; i < 25; i++)
            {
                ConsoleEx.WriteAt(1, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.Cyan);
                ConsoleEx.WriteAt(9, i, "DICTATOR", ConsoleColor.Cyan, ConsoleColor.Black);
                ConsoleEx.WriteAt(17, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.Cyan);
                ConsoleEx.WriteAt(25, i, "DICTATOR", ConsoleColor.Cyan, ConsoleColor.Black);
            }
        }

        public void DrawContent()
        {
            ConsoleEx.WriteAt(1, 3, "Press any key to become DICTATOR", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.WriteAt(13, 5, " of the ", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.WriteAt(13, 8, "RITIMBAN", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(14, 10, "******", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(16, 11, "**", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(14, 12, "******", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(13, 14, "REPUBLIC", ConsoleColor.Green, ConsoleColor.Black);
        }
    }
}
