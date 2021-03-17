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


            Console.BackgroundColor = ConsoleColor.DarkYellow;
            ConsoleEx.Clear();

            if(decisions.IsNotEmpty())
            {
                // TODO: Display options with the original positioning
                int line = 4;

                for (int i = 0; i < decisions.Length; i++)
                {
                    int optionNumber = i + 1;

                    ConsoleEx.WriteAt(1, line + i, $"{optionNumber}.");
                    line++;
                    ConsoleEx.WriteAt(1, line + i, decisions[i].Text);
                    line++;
                }
            }
            else
            {
                ConsoleEx.WriteAt(1, 12, "   ALL of this section USED UP  ");
            }

            Console.ReadKey(true);
        }
    }
}
