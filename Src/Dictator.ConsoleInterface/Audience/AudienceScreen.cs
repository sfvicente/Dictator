using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Audience
{
    public class AudienceScreen : IAudienceScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AudienceScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show(Core.Audience audience)
        {
            ConsoleEx.Clear();
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

            ConsoleEx.WriteAt(1, 11, $" A request from {audience.Requester}", ConsoleColor.DarkYellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 15, " Will YOUR EXCELLENCY agree to  ", ConsoleColor.DarkYellow, ConsoleColor.Black);
            ConsoleEx.WriteAt(1, 17, $"{audience.Text}", ConsoleColor.Yellow, ConsoleColor.Black);
            pressAnyKeyControl.Show();
        }
    }
}
