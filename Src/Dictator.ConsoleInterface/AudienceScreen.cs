using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class AudienceScreen : IAudienceScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AudienceScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            Console.Clear();

            ConsoleEx.WriteEmptyLineAt(1, ConsoleColor.Green);
            ConsoleEx.WriteEmptyLineAt(2, ConsoleColor.Green);
            ConsoleEx.WriteEmptyLineAt(3, ConsoleColor.Green);
            ConsoleEx.WriteEmptyLineAt(4, ConsoleColor.Green);
            ConsoleEx.WriteAt(11, 4, "AN AUDIENCE", ConsoleColor.White, ConsoleColor.Black);
            ConsoleEx.WriteEmptyLineAt(5, ConsoleColor.Green);
         
            for(int row = 6; row < 22; row++)
            {
                ConsoleEx.WriteEmptyLineAt(row, ConsoleColor.DarkYellow);
            }

            ConsoleEx.WriteAt(0, 11, " A request from {Group}", ConsoleColor.DarkYellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(0, 15, " Will YOUR EXCELLENCY agree to  ", ConsoleColor.DarkYellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(0, 17, "<petition text>                 ", ConsoleColor.Yellow, ConsoleColor.Black);

            pressAnyKeyControl.Show();

            Console.ReadKey();
        }
    }
}
