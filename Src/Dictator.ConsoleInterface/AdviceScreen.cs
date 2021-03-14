using Dictator.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class AdviceScreen : IAdviceScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AdviceScreen(IPressAnyKeyControl pressAnyKeyControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.DarkYellow);
            ConsoleEx.WriteAt(1, 2, "{Decision Text}", ConsoleColor.Black, ConsoleColor.DarkYellow);
            ConsoleEx.WriteAt(1, 4, "Your POPULARITY with", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.Write(" ....");

            // TODO: display decision impact on each group popularity

            ConsoleEx.WriteAt(1, 11, "The STRENGTH of", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.Write(" ....");

            // TODO: display decision impact on each group strength

            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
