using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player is given a list of options to make a
    ///     presidential decision.
    /// </summary>
    public class PresidentialDecisionMainDialog : BaseScreen, IPresidentialDecisionMainDialog
    {
        private readonly IPressAnyKeyOrOptionControl pressAnyKeyOrOptionControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PresidentialDecisionMainDialog"/> class from a 
        ///     <see cref="IPressAnyKeyOrOptionControl"/> component.
        /// </summary>
        /// <param name="pressAnyKeyOrOptionControl">The control that is displayed when the user is required to press a key
        /// or select an option.</param>
        public PresidentialDecisionMainDialog(IConsoleService consoleService, IPressAnyKeyOrOptionControl pressAnyKeyOrOptionControl)
            : base(consoleService)
        {
            this.pressAnyKeyOrOptionControl = pressAnyKeyOrOptionControl;
        }

        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <returns>The type of presidential decision that has been selected.</returns>
        public DecisionType Show()
        {
            _consoleService.Clear('*', ConsoleColor.Red, ConsoleColor.Yellow);
            _consoleService.WriteAt(5, 4, "PRESIDENTIAL DECISION", ConsoleColor.Blue, ConsoleColor.White);
            _consoleService.WriteAt(1, 7, "Try to ...", ConsoleColor.Yellow, ConsoleColor.Black);
            _consoleService.WriteAt(4, 9, "1. PLEASE a GROUP       ", ConsoleColor.Yellow, ConsoleColor.Black);
            _consoleService.WriteAt(4, 11, "2. PLEASE ALL GROUPS    ", ConsoleColor.Yellow, ConsoleColor.Black);
            _consoleService.WriteAt(4, 13, "3. IMPROVE your CHANCES ", ConsoleColor.Yellow, ConsoleColor.Black);
            _consoleService.WriteAt(4, 15, "4. RAISE some CASH      ", ConsoleColor.Yellow, ConsoleColor.Black);
            _consoleService.WriteAt(4, 17, "5. STRENGTHEN a GROUP   ", ConsoleColor.Yellow, ConsoleColor.Black);
            
            ConsoleKey keyPressed = pressAnyKeyOrOptionControl.Show();

            switch (keyPressed)
            {
                case ConsoleKey.D1:
                    return DecisionType.PleaseAGroup;
                case ConsoleKey.D2:
                    return DecisionType.PleaseAllGroups;
                case ConsoleKey.D3:
                    return DecisionType.ImproveYourChanges;
                case ConsoleKey.D4:
                    return DecisionType.RaiseSomeCash;
                case ConsoleKey.D5:
                    return DecisionType.StrengthenAGroup;
                default:
                    return DecisionType.None;
            }
        }
    }
}
