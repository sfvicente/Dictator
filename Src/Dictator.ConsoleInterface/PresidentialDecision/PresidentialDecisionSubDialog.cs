using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public class PresidentialDecisionSubDialog : IPresidentialDecisionSubDialog
    {
        private readonly IDecisionStats decisionStats;
        private readonly IPressAnyKeyOrOptionControl pressAnyKeyOrOptionControl;

        public PresidentialDecisionSubDialog(IDecisionStats decisionStats, IPressAnyKeyOrOptionControl pressAnyKeyOrOptionControl)
        {
            this.decisionStats = decisionStats;
            this.pressAnyKeyOrOptionControl = pressAnyKeyOrOptionControl;
        }

        private bool HaveAllDecisionsBeenUsed(Decision[] decisions)
        {
            for (int i = 0; i < decisions.Length; i++)
            {
                if (!decisions[i].HasBeenUsed)
                {
                    return false;
                }
            }

            return true;
        }

        public int Show(DecisionType decisionType)
        {
            Decision[] decisions = decisionStats.GetDecisionsByType(decisionType);

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            ConsoleEx.Clear();

            if(HaveAllDecisionsBeenUsed(decisions))
            {
                ConsoleEx.WriteAt(1, 12, "   ALL of this section USED UP  ");
            }
            else
            {
                // TODO: Display options with the original positioning
                int line = 4;

                for (int i = 0; i < decisions.Length; i++)
                {
                    int optionNumber = i + 1;

                    if (!decisions[i].HasBeenUsed)
                    {
                        ConsoleEx.WriteAt(1, line + i, $"{optionNumber}.");
                    }

                    line++;

                    if (!decisions[i].HasBeenUsed)
                    {
                        ConsoleEx.WriteAt(1, line + i, decisions[i].Text);
                    }

                    line++;
                }
            }
            
            ConsoleKey keyPressed = pressAnyKeyOrOptionControl.Show();

            switch (keyPressed)
            {
                case ConsoleKey.D1:
                    return 1;
                case ConsoleKey.D2:
                    return 2;
                case ConsoleKey.D3:
                    return 3;
                case ConsoleKey.D4:
                    return 4;
                case ConsoleKey.D5:
                    return 5;
                case ConsoleKey.D6:
                    return 6;
                default:
                    return 99;
            }
        }
    }
}
