using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Assassination
{
    public class AssassinationScreen : IAssassinationScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        private readonly IGroupService groupStats;

        public AssassinationScreen(IPressAnyKeyControl pressAnyKeyControl, IGroupService groupStats)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.groupStats = groupStats;
        }

        public void Show()
        {
            GroupType assassinGroupType = groupStats.AssassinGroupType;
            string groupName = groupStats.GetGroupByType(assassinGroupType).Name;
            int startPosition = (32 - groupName.Length) / 2;

            ConsoleEx.Clear();
            ConsoleEx.WriteAt(1, 11, "      ASSASSINATION ATTEMPT     ");
            ConsoleEx.WriteAt(startPosition, 12, $"by one of {groupName}");
            pressAnyKeyControl.Show();
        }
    }
}
