﻿using Dictator.Common;
using System;

namespace Dictator.ConsoleInterface.Common
{
    public class PressAnyKeyWithYesControl : IPressAnyKeyWithYesControl
    {
        private readonly IKeyPanel keyPanel;

        public PressAnyKeyWithYesControl(IKeyPanel keyPanel)
        {
            this.keyPanel = keyPanel;
        }

        /// <summary>
        ///     Displays the control.
        /// </summary>
        /// <returns>The option selected after the control has been presented.</returns>
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
