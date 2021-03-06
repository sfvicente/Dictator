using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class DecisionMainDialog : IDecisionMainDialog
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public DecisionMainDialog(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Red);

            ConsoleEx.WriteAt(24, 1, "********************************");
            ConsoleEx.WriteAt(24, 4, "     PRESIDENTIAL DECISION      ", ConsoleColor.White, ConsoleColor.Blue);
            ConsoleEx.WriteAt(24, 7, " Try to ...                     ", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(24, 9, "    1. PLEASE a GROUP           ", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(24, 11, "    2. PLEASE ALL GROUPS        ", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(24, 13, "    3. IMPROVE your CHANCES     ", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(24, 15, "    4. RAISE some CASH          ", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(24, 17, "    5. STRENGTHEN a GROUP       ", ConsoleColor.Yellow, ConsoleColor.Black);

            pressAnyKeyControl.Show();
            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            switch(keyPressed)
            {
                case ConsoleKey.D1:
                    return;
                case ConsoleKey.D2:
                    return;
                case ConsoleKey.D3:
                    return;
                case ConsoleKey.D4:
                    return;
                case ConsoleKey.D5:
                    return;
                default:
                    return;
            }
        }
    }
}
