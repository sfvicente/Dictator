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

            pressAnyKeyControl.Show();
            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            switch (keyPressed)
            {
                case ConsoleKey.D1:
                    if (OptionExistsAndIsAvailable(decisions, 1))
                        return;
                    return;
                case ConsoleKey.D2:
                    if (OptionExistsAndIsAvailable(decisions, 2))
                        return;
                    return;
                case ConsoleKey.D3:
                    if (OptionExistsAndIsAvailable(decisions, 3))
                        return;
                    return;
                case ConsoleKey.D4:
                    if (OptionExistsAndIsAvailable(decisions, 4))
                        return;
                    return;
                case ConsoleKey.D5:
                    if (OptionExistsAndIsAvailable(decisions, 5))
                        return;
                    return;
                case ConsoleKey.D6:
                    if (OptionExistsAndIsAvailable(decisions, 6))
                        return;
                    return;
                default:
                    return;
            }
        }

        private bool OptionExistsAndIsAvailable(Decision[] decisions, int optionNumber)
        {
            if(optionNumber > decisions.Length)
            {
                return false;
            }

            if (decisions[optionNumber - 1].HasBeenUsed)
            {
                return false;
            }

            return true;
        }
    }
}
