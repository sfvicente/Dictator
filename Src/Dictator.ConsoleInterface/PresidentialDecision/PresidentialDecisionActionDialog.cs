using Dictator.ConsoleInterface.Common;
using Dictator.Core.Models;
using System;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player needs to make a decision to accept or refuse
    ///     the presidential option that has been selected.
    /// </summary>
    public class PresidentialDecisionActionDialog : IPresidentialDecisionActionDialog
    {
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PresidentialDecisionActionDialog"/> class from a 
        ///     <see cref="IPressAnyKeyWithYesControl"/> component.
        /// </summary>
        /// <param name="pressAnyKeyWithYesControl">The control that is displayed when the user is required to press a key
        /// or select yes.</param>
        public PresidentialDecisionActionDialog(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <param name="decision">The decision to be displayed to the player for acceptance or refusal.</param>
        /// <returns>The option selected after the dialog has been presented.</returns>
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
