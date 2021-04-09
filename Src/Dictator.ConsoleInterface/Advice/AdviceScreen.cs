﻿using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Advice
{
    public class AdviceScreen : IAdviceScreen
    {
        private readonly IGroupStats groupStats;
        private readonly IAudienceStats audienceStats;
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AdviceScreen(IGroupStats groupStats, IAudienceStats audienceStats, IPressAnyKeyControl pressAnyKeyControl)
        {
            this.groupStats = groupStats;
            this.audienceStats = audienceStats;
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public void Show()
        {
            Core.Audience audience = audienceStats.CurrentAudienceRequest;

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 2, $"{audience.Text}", ConsoleColor.Black, ConsoleColor.DarkYellow);
            ConsoleEx.WriteAt(1, 4, "Your POPULARITY with", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.Write(" ....", ConsoleColor.Black);
            DisplayPopularityChanges(audience.GroupPopularityChanges);
            ConsoleEx.WriteAt(1, Console.CursorTop + 3, "The STRENGTH of", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.Write(" ...", ConsoleColor.Black);
            DisplayGroupStrengthChanges(audience.GroupStrengthChanges);
            pressAnyKeyControl.Show();
        }

        public void Show(Decision decision)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 2, $"{decision.Text}", ConsoleColor.Black, ConsoleColor.DarkYellow);
            ConsoleEx.WriteAt(1, 4, "Your POPULARITY with", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.Write(" ....", ConsoleColor.Black);
            DisplayPopularityChanges(decision.GroupPopularityChanges);
            ConsoleEx.WriteAt(1, Console.CursorTop + 3, "The STRENGTH of", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.Write(" ...", ConsoleColor.Black);
            DisplayGroupStrengthChanges(decision.GroupStrengthChanges);
            pressAnyKeyControl.Show();

        }

        public void DisplayPopularityChanges(string groupPopularityChanges)
        {
            int line = 6;

            for (int i = 0; i < 8; i++)
            {
                if (groupPopularityChanges[i] != 'M')
                {
                    int popularityChange = groupPopularityChanges[i] - 'M';
                    
                    ConsoleEx.WriteAt(1, line, $"  {groupStats.GetGroupNameByIndex(i)}", ConsoleColor.Black);
                    ConsoleEx.WriteAt(22, line, $"{GetFormattedChange(popularityChange)}", ConsoleColor.Black);
                    line++;
                }
            }
        }

        public void DisplayGroupStrengthChanges(string groupStrengthChanges)
        {
            int line = Console.CursorTop + 3;

            for (int i = 0; i < 6; i++)
            {
                if (groupStrengthChanges[i] != 'M')
                {
                    int strengthChange = groupStrengthChanges[i] - 'M';

                    ConsoleEx.WriteAt(1, line, $"  {groupStats.GetGroupNameByIndex(i)}", ConsoleColor.Black);
                    ConsoleEx.WriteAt(22, line, $"{GetFormattedChange(strengthChange)}", ConsoleColor.Black);
                    line++;
                }
            }
        }

        public string GetFormattedChange(int change)
        {
            if(change > 0)
            {
                return "+" + change;
            }

            return change.ToString();
        }
    }
}
