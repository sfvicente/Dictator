using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class EndScreen : IEndScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        private readonly IAccount account;
        private readonly IGovernmentStats governmentStats;
        private readonly IGroupStats groupStats;

        public EndScreen(IPressAnyKeyControl pressAnyKeyControl, IAccount account, IGovernmentStats governmentStats, IGroupStats groupStats)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.account = account;
            this.governmentStats = governmentStats;
            this.groupStats = groupStats;
        }

        public void Show()
        {
            ConsoleEx.WriteAt(24, 3, "    Your RATING as PRESIDENT    ");
            ConsoleEx.WriteAt(24, 5, " Total POPULARITY         - {totalPopularity}  ");
            ConsoleEx.WriteAt(24, 7, " MONTHS in OFFICE ({month}x3)");

            // TODO: count alive score

            // TODO: save history if it is a new highscore

            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
