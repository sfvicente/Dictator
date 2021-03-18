﻿using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class AdviceRequestDialog : IAdviceRequestDialog
    {
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        public AdviceRequestDialog(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        public DialogResult Show()
        {
            ConsoleEx.Clear(ConsoleColor.Green);
            
            for(int row = 2; row < 21; row++)
            {
                ConsoleEx.WriteAt(11, row, "?", ConsoleColor.Black, ConsoleColor.White);
                ConsoleEx.WriteAt(12, row, " ADVICE ", ConsoleColor.Gray, ConsoleColor.Black);
                ConsoleEx.WriteAt(20, row, "?", ConsoleColor.Black, ConsoleColor.White);
            }

            pressAnyKeyWithYesControl.Show();

            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            if(keyPressed == ConsoleKey.Y)
            {
                return DialogResult.Yes;
            }

            return DialogResult.No;
        }
    }
}