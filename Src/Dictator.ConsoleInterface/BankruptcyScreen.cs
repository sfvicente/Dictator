﻿using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class BankruptcyScreen: IBankruptcyScreen
    {
        private readonly IGroupStats groupStats;
        private readonly IGovernmentStats governmentStats;

        public BankruptcyScreen(IGroupStats groupStats, IGovernmentStats governmentStats)
        {
            this.groupStats = groupStats;
            this.governmentStats = governmentStats;
        }

        public void Show()
        {
            Console.Clear();

            ConsoleEx.WriteAt(24, 5, "   The TREASURY is BANKRUPT    ");
            ConsoleEx.WriteAt(24, 9, "Your POPULARITY with the ARMY &");
            ConsoleEx.WriteAt(24, 11, " the SECRET POLICE will DROP ! ");
            ConsoleEx.WriteAt(24, 13, "    POLICE STRENGTH will drop  ");
            ConsoleEx.WriteAt(24, 15, "and YOUR own STRENGTH will DROP");

            groupStats.DecreasePopularity(GroupType.Army);
            groupStats.DecreasePopularity(GroupType.SecretPolice);
            groupStats.DecreaseStrength(GroupType.SecretPolice);
            governmentStats.DecreasePlayerStrength();
            Console.ReadKey(true);
        }
    }
}
