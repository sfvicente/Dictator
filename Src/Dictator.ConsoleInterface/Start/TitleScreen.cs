using Dictator.Common.Extensions;
using System;

namespace Dictator.ConsoleInterface.Start
{
    public class TitleScreen : ITitleScreen
    {
        public void Show()
        {
            ConsoleEx.Clear();
            PrintBackground();
            PrintContent();
            Console.ReadKey(true);
        }

        public void PrintBackground()
        {
            for (int i = 1; i < 25; i++)
            {
                ConsoleEx.WriteAt(1, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.Cyan);
                ConsoleEx.WriteAt(9, i, "DICTATOR", ConsoleColor.Cyan, ConsoleColor.Black);
                ConsoleEx.WriteAt(17, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.Cyan);
                ConsoleEx.WriteAt(25, i, "DICTATOR", ConsoleColor.Cyan, ConsoleColor.Black);
            }
        }

        public void PrintContent()
        {
            ConsoleEx.WriteAt(1, 4, "Press any key to become DICTATOR", ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(13, 6, " of the ", ConsoleColor.Gray, ConsoleColor.Black);
            PrintFlagLine(9);
            PrintFlagLine(10);
            ConsoleEx.WriteAt(8, 11, "    ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(12, 11, " ", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(13, 11, "RITIMBAN", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(21, 11, " ", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(22, 11, "    ", ConsoleColor.Green, ConsoleColor.Black);
            PrintFlagLine(12);
            ConsoleEx.WriteAt(8, 13, "    ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(12, 13, "  ******  ", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(22, 13, "    ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(8, 14, "    ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(12, 14, "    **    ", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(22, 14, "    ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(8, 15, "    ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(12, 15, "  ******  ", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(22, 15, "    ", ConsoleColor.Green, ConsoleColor.Black);
            PrintFlagLine(16);
            ConsoleEx.WriteAt(8, 17, "    ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(12, 17, " ", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(13, 17, "REPUBLIC", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(21, 17, " ", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(22, 17, "    ", ConsoleColor.Green, ConsoleColor.Black);
            PrintFlagLine(18);
            PrintFlagLine(19);
            PrintFlagLine(20);
        }

        private void PrintFlagLine(int row)
        {
            ConsoleEx.WriteAt(8, row, "    ", ConsoleColor.Green, ConsoleColor.Black);
            ConsoleEx.WriteAt(12, row, "          ", ConsoleColor.Blue, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(22, row, "    ", ConsoleColor.Green, ConsoleColor.Black);
        }
    }
}
