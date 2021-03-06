﻿using Dictator.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Common
{
    public class PressAnyKeyWithYesControl : IPressAnyKeyWithYesControl
    {
        private readonly IKeyPanel keyPanel;

        public PressAnyKeyWithYesControl(IKeyPanel keyPanel)
        {
            this.keyPanel = keyPanel;
        }

        public DialogResult Show()
        {
            ConsoleEx.WriteAt(11, 21, " \"Y\"= YES ", ConsoleColor.White, ConsoleColor.Black);
            keyPanel.Show();

            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            if (keyPressed == ConsoleKey.Y)
            {
                return DialogResult.Yes;
            }

            return DialogResult.No;
        }
    }
}
