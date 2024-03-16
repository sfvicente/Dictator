﻿using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the screen that is displayed when the player does not have enough popularity
    ///     with a group for them to accept being their ally in a revolution.
    /// </summary>
    public class RevolutionAllyLowPopularityScreen : BaseScreen, IRevolutionAllyLowPopularityScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public RevolutionAllyLowPopularityScreen(IConsoleService consoleService, IPressAnyKeyControl pressAnyKeyControl): base(consoleService)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
            _consoleService.WriteAt(1, 12, "      You must be JOKING !      ");
            pressAnyKeyControl.Show();
        }
    }
}
