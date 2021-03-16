using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class AdviceScreen : IAdviceScreen
    {
        private readonly IAudienceStats audienceStats;
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public AdviceScreen(IAudienceStats audienceStats, IPressAnyKeyControl pressAnyKeyControl)
        {
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

            // TODO: display decision impact on each group popularity

            ConsoleEx.WriteAt(1, 11, "The STRENGTH of", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.Write(" ....", ConsoleColor.Black);

            // TODO: display decision impact on each group strength

            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
