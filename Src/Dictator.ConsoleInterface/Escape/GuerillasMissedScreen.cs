﻿using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape
{
    public class GuerillasMissedScreen : IGuerillasMissedScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public GuerillasMissedScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 11, " The GUERILLAS didn't catch you ");
            pressAnyKeyControl.Show();
        }
    }
}
