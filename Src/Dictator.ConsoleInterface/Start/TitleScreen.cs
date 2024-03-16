using System;

namespace Dictator.ConsoleInterface.Start
{
    /// <summary>
    ///     Represents the screen that is displayed with the title of the game.
    /// </summary>
    public class TitleScreen : BaseScreen, ITitleScreen
    {
        public TitleScreen(IConsoleService consoleService)
            : base(consoleService)
        {
            
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public override void Show()
        {
            _consoleService.Clear();
            PrintBackground();
            PrintContent();
            Console.ReadKey(true);
        }

        private void PrintBackground()
        {
            for (int i = 1; i < 25; i++)
            {
                _consoleService.WriteAt(1, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.Cyan);
                _consoleService.WriteAt(9, i, "DICTATOR", ConsoleColor.Cyan, ConsoleColor.Black);
                _consoleService.WriteAt(17, i, "DICTATOR", ConsoleColor.Black, ConsoleColor.Cyan);
                _consoleService.WriteAt(25, i, "DICTATOR", ConsoleColor.Cyan, ConsoleColor.Black);
            }
        }

        private void PrintContent()
        {
            _consoleService.WriteAt(1, 4, "Press any key to become DICTATOR", ConsoleColor.Gray, ConsoleColor.Black);
            _consoleService.WriteAt(13, 6, " of the ", ConsoleColor.Gray, ConsoleColor.Black);
            PrintFlagLine(9);
            PrintFlagLine(10);
            _consoleService.WriteAt(8, 11, "    ", ConsoleColor.Green, ConsoleColor.Black);
            _consoleService.WriteAt(12, 11, " ", ConsoleColor.Blue, ConsoleColor.Yellow);
            _consoleService.WriteAt(13, 11, "RITIMBAN", ConsoleColor.Green, ConsoleColor.Black);
            _consoleService.WriteAt(21, 11, " ", ConsoleColor.Blue, ConsoleColor.Yellow);
            _consoleService.WriteAt(22, 11, "    ", ConsoleColor.Green, ConsoleColor.Black);
            PrintFlagLine(12);
            _consoleService.WriteAt(8, 13, "    ", ConsoleColor.Green, ConsoleColor.Black);
            _consoleService.WriteAt(12, 13, "  ******  ", ConsoleColor.Blue, ConsoleColor.Yellow);
            _consoleService.WriteAt(22, 13, "    ", ConsoleColor.Green, ConsoleColor.Black);
            _consoleService.WriteAt(8, 14, "    ", ConsoleColor.Green, ConsoleColor.Black);
            _consoleService.WriteAt(12, 14, "    **    ", ConsoleColor.Blue, ConsoleColor.Yellow);
            _consoleService.WriteAt(22, 14, "    ", ConsoleColor.Green, ConsoleColor.Black);
            _consoleService.WriteAt(8, 15, "    ", ConsoleColor.Green, ConsoleColor.Black);
            _consoleService.WriteAt(12, 15, "  ******  ", ConsoleColor.Blue, ConsoleColor.Yellow);
            _consoleService.WriteAt(22, 15, "    ", ConsoleColor.Green, ConsoleColor.Black);
            PrintFlagLine(16);
            _consoleService.WriteAt(8, 17, "    ", ConsoleColor.Green, ConsoleColor.Black);
            _consoleService.WriteAt(12, 17, " ", ConsoleColor.Blue, ConsoleColor.Yellow);
            _consoleService.WriteAt(13, 17, "REPUBLIC", ConsoleColor.Green, ConsoleColor.Black);
            _consoleService.WriteAt(21, 17, " ", ConsoleColor.Blue, ConsoleColor.Yellow);
            _consoleService.WriteAt(22, 17, "    ", ConsoleColor.Green, ConsoleColor.Black);
            PrintFlagLine(18);
            PrintFlagLine(19);
            PrintFlagLine(20);
        }

        private void PrintFlagLine(int row)
        {
            _consoleService.WriteAt(8, row, "    ", ConsoleColor.Green, ConsoleColor.Black);
            _consoleService.WriteAt(12, row, "          ", ConsoleColor.Blue, ConsoleColor.Yellow);
            _consoleService.WriteAt(22, row, "    ", ConsoleColor.Green, ConsoleColor.Black);
        }
    }
}
