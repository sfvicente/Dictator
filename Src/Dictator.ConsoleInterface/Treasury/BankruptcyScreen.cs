using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Treasury
{
    public class BankruptcyScreen: IBankruptcyScreen
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        private readonly IGroupStats groupStats;
        private readonly IGovernmentStats governmentStats;

        public BankruptcyScreen(IPressAnyKeyControl pressAnyKeyControl, IGroupStats groupStats, IGovernmentStats governmentStats)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.groupStats = groupStats;
            this.governmentStats = governmentStats;
        }

        public void Show()
        {
            ConsoleEx.Clear();

            ConsoleEx.WriteAt(1, 5, "   The TREASURY is BANKRUPT    ");
            ConsoleEx.WriteAt(1, 9, "Your POPULARITY with the ARMY &");
            ConsoleEx.WriteAt(1, 11, " the SECRET POLICE will DROP ! ");
            ConsoleEx.WriteAt(1, 13, "    POLICE STRENGTH will drop  ");
            ConsoleEx.WriteAt(1, 15, "and YOUR own STRENGTH will DROP");
            pressAnyKeyControl.Show();
        }
    }
}
