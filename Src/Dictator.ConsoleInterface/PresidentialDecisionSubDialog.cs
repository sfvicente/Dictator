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
        private readonly IPressAnyKeyControl pressAnyKeyControl;

        public PresidentialDecisionSubDialog(IDecisionStats decisionStats, IPressAnyKeyControl pressAnyKeyControl)
        {
            this.decisionStats = decisionStats;
            this.pressAnyKeyControl = pressAnyKeyControl;
        }

        public int Show(DecisionType decisionType)
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

            pressAnyKeyControl.Show();
            ConsoleKey keyPressed = Console.ReadKey(true).Key;

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
