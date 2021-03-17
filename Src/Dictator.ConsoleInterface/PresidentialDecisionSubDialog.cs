using Dictator.Common.Extensions;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class PresidentialDecisionSubDialog : IPresidentialDecisionSubDialog
    {
        private readonly IDecisionStats decisionStats;

        public PresidentialDecisionSubDialog(IDecisionStats decisionStats)
        {
            this.decisionStats = decisionStats;
        }

        public void Show(DecisionType decisionType)
        {
            Decision[] decisions = decisionStats.GetDecisionsByType(decisionType);

            // TODO: Display screen with appropriate options
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            ConsoleEx.Clear();

            Console.ReadKey(true);
        }


    }
}
