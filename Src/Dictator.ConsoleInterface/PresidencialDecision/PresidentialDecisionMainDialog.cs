using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PresidentialDecisionMainDialog : IPresidentialDecisionMainDialog
    {
        private readonly IPressAnyKeyOrOptionControl pressAnyKeyOrOptionControl;

        public PresidentialDecisionMainDialog(IPressAnyKeyOrOptionControl pressAnyKeyOrOptionControl)
        {
            this.pressAnyKeyOrOptionControl = pressAnyKeyOrOptionControl;
        }

        public DecisionType Show()
        {
            ConsoleEx.Clear('*', ConsoleColor.Red, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(5, 4, "PRESIDENTIAL DECISION", ConsoleColor.Blue, ConsoleColor.White);
            ConsoleEx.WriteAt(1, 7, "Try to ...", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(4, 9, "1. PLEASE a GROUP       ", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(4, 11, "2. PLEASE ALL GROUPS    ", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(4, 13, "3. IMPROVE your CHANCES ", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(4, 15, "4. RAISE some CASH      ", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(4, 17, "5. STRENGTHEN a GROUP   ", ConsoleColor.Yellow, ConsoleColor.Black);
            
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
