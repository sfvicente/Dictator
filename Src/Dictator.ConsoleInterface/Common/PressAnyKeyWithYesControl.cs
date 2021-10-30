﻿using Dictator.Common;
using System;

namespace Dictator.ConsoleInterface.Common
{
    /// <summary>
    ///     Represents the control that is displayed with a panel when the user is required
    ///     to press a key or select yes.
    /// </summary>
    public class PressAnyKeyWithYesControl : IPressAnyKeyWithYesControl
    {
        private readonly IKeyPanel keyPanel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PressAnyKeyWithYesControl"/> class from a <see cref="IKeyPanel"/>
        ///     component.
        /// </summary>
        /// <param name="keyPanel">The panel that is displayed when the user is required to press a key.</param>
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
