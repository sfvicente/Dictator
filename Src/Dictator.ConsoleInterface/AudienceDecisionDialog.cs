using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class AudienceDecisionDialog : IAudienceDecisionDialog
    {
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        public AudienceDecisionDialog(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        public DialogResult Show()
        {
            ConsoleEx.Clear(ConsoleColor.DarkYellow);
            ConsoleEx.WriteAt(11, 2, "  DECISION  ", ConsoleColor.Black, ConsoleColor.White);
            ConsoleEx.WriteAt(5, 4, "The {GroupName} ask you to", ConsoleColor.Magenta, ConsoleColor.Gray);
            ConsoleEx.WriteAt(5, 4, "{RequestText}", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteEmptyLineAt(8, ConsoleColor.Blue);

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            ConsoleEx.WriteAt(2, 10, "This decision would", ConsoleColor.Black);
            ConsoleEx.WriteAt(2, 12, "TAKE from the TREASURY $120,000", ConsoleColor.Black);
            ConsoleEx.WriteAt(2, 14, "and", ConsoleColor.Black);
            ConsoleEx.WriteAt(2, 16, "RAISE MONTHLY COSTS bt $10,000", ConsoleColor.Black);

            pressAnyKeyWithYesControl.Show();

            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            if (keyPressed == ConsoleKey.Y)
            {
                return DialogResult.Yes;
            }

            return DialogResult.No;
        }
    }
}
