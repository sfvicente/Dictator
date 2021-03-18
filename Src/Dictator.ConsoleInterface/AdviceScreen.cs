using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
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
            Audience audience = audienceStats.CurrentAudienceRequest;

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 2, $"{audience.Text}", ConsoleColor.Black, ConsoleColor.DarkYellow);
            ConsoleEx.WriteAt(1, 4, "Your POPULARITY with", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.Write(" ....", ConsoleColor.Black);

            DisplayPopularityChanges(audience);

            ConsoleEx.WriteAt(1, 11, "The STRENGTH of", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.Write(" ....", ConsoleColor.Black);

            // TODO: display decision impact on each group strength

            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }

        public void DisplayPopularityChanges(Audience audience)
        {
            int line = 6;

            for (int i = 0; i < 8; i++)
            {
                if (audience.GroupPopularityChanges[i] != 'M')
                {
                    int popularityChange = audience.GroupPopularityChanges[i] - 'M';
                    
                    ConsoleEx.WriteAt(1, line, $"  {groupStats.GetGroupNameByIndex(i)}", ConsoleColor.Black);
                    ConsoleEx.WriteAt(22, line, $"{GetFormattedChange(popularityChange)}", ConsoleColor.Black);
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
