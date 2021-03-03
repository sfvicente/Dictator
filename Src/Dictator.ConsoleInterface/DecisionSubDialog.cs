using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public class DecisionSubDialog : IDecisionSubDialog
    {
        private readonly IDecisionStats decisionStats;

        public DecisionSubDialog(IDecisionStats decisionStats)
        {
            this.decisionStats = decisionStats;
        }

        public void Show(DecisionType decisionType)
        {
            Decision[] decisions = decisionStats.GetDecisionsByType(decisionType);

            // TODO: Display screen with appropriate options
        }


    }
}
