using Dictator.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Month
{
    public class MonthScreen : IMonthScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="month">The number of the month to display on the screen.</param>
        public void Show(int month)
        {
            ConsoleEx.Clear(ConsoleColor.Yellow);
            ConsoleEx.WriteAt(8, 10, "MONTH ", ConsoleColor.Cyan, ConsoleColor.Black);
            ConsoleEx.WriteAt(14, 10, $"{month}", ConsoleColor.White, ConsoleColor.Black);
            Console.ReadKey(true);
        }
    }
}
