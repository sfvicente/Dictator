using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class AssassinationScreen : IAssassinationScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        private readonly IGroupStats groupStats;
        private readonly IGovernmentStats governmentStats;

        public AssassinationScreen(IPressAnyKeyControl pressAnyKeyControl, IGroupStats groupStats, IGovernmentStats governmentStats)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.groupStats = groupStats;
            this.governmentStats = governmentStats;
        }

        public void Show()
        {
            GroupType assassinGroupType = groupStats.AssassinGroupType;
            string groupName = groupStats.GetGroupByType(assassinGroupType).Name;
            int startPosition = (32 - groupName.Length) - 10 / 2;

            ConsoleEx.WriteAt(0, 11, "      ASSASSINATION ATTEMPT     ");
            ConsoleEx.WriteAt(startPosition, 12, $"by one of {groupName}");
            pressAnyKeyControl.Show();
            Console.ReadKey(true);
        }
    }
}
