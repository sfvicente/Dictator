﻿using Dictator.Common;
using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterface.Treasury
{
    /// <summary>
    ///     Represents the screen that is displayed when the treasury is bankrupt, leading to a drop in
    ///     the player's popularity.
    /// </summary>
    public class BankruptcyScreen: IBankruptcyScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BankruptcyScreen"/> class from a <see cref="IPressAnyKeyControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public BankruptcyScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show()
        {
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 5, "   The TREASURY is BANKRUPT    ");
            ConsoleEx.WriteAt(1, 9, "Your POPULARITY with the ARMY &");
            ConsoleEx.WriteAt(1, 11, " the SECRET POLICE will DROP ! ");
            ConsoleEx.WriteAt(1, 13, "    POLICE STRENGTH will drop  ");
            ConsoleEx.WriteAt(1, 15, "and YOUR own STRENGTH will DROP");
            pressAnyKeyControl.Show();
        }
    }
}
