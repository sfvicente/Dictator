using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Audience
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player needs to decide on
    ///     the decision of the petition by one of the groups. It includes the treasury
    ///     costs involved.
    /// </summary>
    public class AudienceDecisionDialog : IAudienceDecisionDialog
    {
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AudienceDecisionDialog"/> class from a <see cref="IPressAnyKeyWithYesControl"/>
        ///     component.
        /// </summary>
        /// <param name="pressAnyKeyControl">The control that is displayed when the user is required to press a key.</param>
        public AudienceDecisionDialog(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }
        
        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <param name="audience">The audience information to display on the screen.</param>
        /// <returns>The option selected after the dialog has been presented.</returns>
        public DialogResult Show(Core.Audience audience)
        {
            ConsoleEx.Clear(ConsoleColor.DarkYellow);
            ConsoleEx.WriteAt(11, 2, "  DECISION  ", ConsoleColor.Black, ConsoleColor.White);
            ConsoleEx.WriteAt(5, 4, $"The {audience.Requester} ask you to", ConsoleColor.Magenta, ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 6, $"{audience.Text}", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteEmptyLineAt(8, ConsoleColor.Blue);

            Console.BackgroundColor = ConsoleColor.DarkYellow;

            if (audience.NoMoneyInvolved)
            {
                ConsoleEx.WriteAt(1, 11, "        NO MONEY INVOLVED       ", ConsoleColor.Black);
            }
            else
            {
                ConsoleEx.WriteAt(2, 10, "This decision would", ConsoleColor.Black);

                if (audience.Cost != 0)
                {
                    string addOrTake = (audience.Cost > 0) ? "ADD to" : "TAKE from";

                    ConsoleEx.WriteAt(2, 12, $"{addOrTake} the TREASURY ${Math.Abs(audience.Cost)},000", ConsoleColor.Black);
                }

                if (audience.Cost != 0 && audience.MonthlyCost != 0)
                {
                    ConsoleEx.WriteAt(2, 14, "and", ConsoleColor.Black);
                }

                if (audience.MonthlyCost != 0)
                {
                    string raiseOrLower = (audience.MonthlyCost < 0) ? "RAISE" : "LOWER";

                    ConsoleEx.WriteAt(2, 16, $"{raiseOrLower} MONTHLY COSTS by ${Math.Abs(audience.MonthlyCost)},000", ConsoleColor.Black);
                }
            }

            return pressAnyKeyWithYesControl.Show();
        }
    }
}
