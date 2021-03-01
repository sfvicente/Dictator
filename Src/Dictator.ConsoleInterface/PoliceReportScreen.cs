using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PoliceReportScreen : IPoliceReportScreen
    {
        private IGovernmentStats governmentStats { get; }
        private IGroupStats groupStats { get; }

        public PoliceReportScreen(IGovernmentStats governmentStats, IGroupStats groupStats)
        {
            this.governmentStats = governmentStats;
            this.groupStats = groupStats;
        }

        public void Draw()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleEx.WriteAt(24, 1, "################################");
            ConsoleEx.WriteAt(24, 3, "      SECRET POLICE REPORT      ");

            ConsoleEx.WriteAt(24, 0, $"MONTH {this.governmentStats.Month}                       ");
            ConsoleEx.WriteAt(24, 3, "         POLICE  REPORT        ");
            ConsoleEx.WriteAt(24, 6, " POPULARITY          STRENGTHS ");

            this.DrawGroups();

            ConsoleEx.WriteAt(24, 17, $"  Your STRENGTH is {this.governmentStats.PlayerStrength}           ");
            ConsoleEx.WriteAt(24, 19, $"  STRENGTH for REVOLUTION is {this.governmentStats.MonthlyRevolutionStrength} ");

            Console.ReadKey();
        }

        public void DrawGroups()
        {
            Group[] groups = this.groupStats.GetGroups();

            for(int i = 0; i < groups.Length; i++)
            {
                int currentPopularity = groups[i].Popularity;
                int popularityStartIndex = 24 + 10 - currentPopularity;

                Console.SetCursorPosition(popularityStartIndex, 8 + i);

                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                for (int p = currentPopularity; p > 0; p--)
                {
                    Console.Write(p);
                }

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(i);

                // TODO: Print group status in colors

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                ConsoleEx.WriteAt(35, 8 + i, $"{groups[i].DisplayName}");

                // TODO: Print group status in colors
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;

                for (int s = 1; s <= groups[i].Strength; s++)
                {
                    Console.Write(s);
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
