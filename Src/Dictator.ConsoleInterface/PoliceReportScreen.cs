﻿using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PoliceReportScreen : IPoliceReportScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        private IGovernmentStats governmentStats { get; }
        private IGroupStats groupStats { get; }

        public PoliceReportScreen(IPressAnyKeyControl pressAnyKeyControl, IGovernmentStats governmentStats, IGroupStats groupStats)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.governmentStats = governmentStats;
            this.groupStats = groupStats;
        }

        public void Show()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleEx.Clear();
            DisplayHeaders();
            DisplayGroups();
            DisplayGovernmentStats();
            pressAnyKeyControl.Show();
            Console.ReadKey();
        }

        private void DisplayHeaders()
        {
            ConsoleEx.WriteAt(1, 2, "################################");
            ConsoleEx.WriteAt(1, 3, "      SECRET POLICE REPORT      ");
            ConsoleEx.WriteAt(1, 1, $"MONTH {governmentStats.Month}                       ");
            ConsoleEx.WriteAt(1, 3, "         POLICE  REPORT        ");
            ConsoleEx.WriteAt(1, 6, " POPULARITY          STRENGTHS ");
        }

        private void DisplayGroups()
        {
            Group[] groups = groupStats.GetGroups();

            for (int i = 0; i < groups.Length; i++)
            {
                int currentPopularity = groups[i].Popularity;
                int popularityStartIndex = 10 - currentPopularity;

                ConsoleEx.SetCursorPosition(popularityStartIndex, 8 + i);

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
                ConsoleEx.WriteAt(11, 8 + i, $"{groups[i].DisplayName}");

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

        private void DisplayGovernmentStats()
        {
            ConsoleEx.WriteAt(1, 17, $"  Your STRENGTH is {governmentStats.PlayerStrength}           ");
            ConsoleEx.WriteAt(1, 19, $"  STRENGTH for REVOLUTION is {governmentStats.MonthlyRevolutionStrength} ");
        }
    }
}
