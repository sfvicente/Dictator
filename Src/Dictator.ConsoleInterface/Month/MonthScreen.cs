using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Month
{
    public class MonthScreen : IMonthScreen
    {
        public void Show(int month)
        {
            ConsoleEx.Clear(ConsoleColor.Yellow);
            ConsoleEx.WriteAt(8, 10, "MONTH ", ConsoleColor.Cyan, ConsoleColor.Black);
            ConsoleEx.WriteAt(14, 10, $"{month}", ConsoleColor.White, ConsoleColor.Black);
            Console.ReadKey(true);
        }
    }
}
