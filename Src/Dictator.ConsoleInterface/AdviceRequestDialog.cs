using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class AdviceRequestDialog : IAdviceRequestDialog
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AdviceRequestDialog(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public DialogResult Show()
        {
            ConsoleEx.Clear(ConsoleColor.Green);
            
            for(int row = 2; row < 21; row++)
            {
                ConsoleEx.WriteAt(24 + 11, row, "?", ConsoleColor.Black, ConsoleColor.White);
                ConsoleEx.WriteAt(24 + 12, row, " ADVICE ", ConsoleColor.White, ConsoleColor.Black);
                ConsoleEx.WriteAt(24 + 20, row, "?", ConsoleColor.Black, ConsoleColor.White);

            }

            pressAnyKeyControl.Show();

            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            if(keyPressed == ConsoleKey.Y)
            {
                return DialogResult.Yes;
            }

            return DialogResult.No;
        }
    }
}
