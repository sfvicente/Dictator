using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class MonthScreen : IMonthScreen
    {
        private readonly IGovernmentStats governmentStats;

        public MonthScreen(IGovernmentStats governmentStats)
        {
            this.governmentStats = governmentStats;
        }

        public void Show()
        {
            ConsoleEx.Clear(ConsoleColor.Yellow);
            ConsoleEx.WriteAt(8, 10, "MONTH ", ConsoleColor.Cyan, ConsoleColor.Black);
            ConsoleEx.WriteAt(14, 10, $"{governmentStats.Month}", ConsoleColor.White, ConsoleColor.Black);
            Console.ReadKey(true);
        }
    }
}
