﻿using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public class PresidentialDecisionActionDialog : IPresidentialDecisionActionDialog
    {
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        public PresidentialDecisionActionDialog(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        public DialogResult Show(Decision decision)
        {
            ConsoleEx.Clear(ConsoleColor.DarkYellow);
            ConsoleEx.WriteAt(1, 5, $"{decision.Text}", ConsoleColor.Yellow, ConsoleColor.Black);

            Console.BackgroundColor = ConsoleColor.DarkYellow;

            if (decision.Cost == 0 && decision.MonthlyCost == 0)
            {
                ConsoleEx.WriteAt(1, 11, "        NO MONEY INVOLVED       ", ConsoleColor.Black);
            }
            else
            {
                ConsoleEx.WriteAt(2, 10, "This decision would", ConsoleColor.Black);

                if (decision.Cost != 0)
                {
                    string addOrTake = (decision.Cost > 0) ? "ADD to" : "TAKE from";

                    ConsoleEx.WriteAt(2, 12, $"{addOrTake} the TREASURY ${Math.Abs(decision.Cost)},000", ConsoleColor.Black);
                }

                if (decision.Cost != 0 && decision.MonthlyCost != 0)
                {
                    ConsoleEx.WriteAt(2, 14, "and", ConsoleColor.Black);
                }

                if (decision.MonthlyCost != 0)
                {
                    string raiseOrLower = (decision.MonthlyCost < 0) ? "RAISE" : "LOWER";

                    ConsoleEx.WriteAt(2, 16, $"{raiseOrLower} MONTHLY COSTS by ${Math.Abs(decision.MonthlyCost)},000", ConsoleColor.Black);
                }
            }

            return pressAnyKeyWithYesControl.Show();
        }
    }
}
