using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Audience
{
    public class AudienceDecisionDialog : IAudienceDecisionDialog
    {
        private readonly IAudienceStats audienceStats;
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        public AudienceDecisionDialog(IAudienceStats audienceStats, IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.audienceStats = audienceStats;
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        public DialogResult Show(Core.Audience audience)
        {
            ConsoleEx.Clear(ConsoleColor.DarkYellow);
            ConsoleEx.WriteAt(11, 2, "  DECISION  ", ConsoleColor.Black, ConsoleColor.White);
            ConsoleEx.WriteAt(5, 4, $"The {audience.Requester} ask you to", ConsoleColor.Magenta, ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 6, $"{audience.Text}", ConsoleColor.Yellow, ConsoleColor.Black);
            ConsoleEx.WriteEmptyLineAt(8, ConsoleColor.Blue);

            Console.BackgroundColor = ConsoleColor.DarkYellow;

            if (audience.Cost != 0 || audience.MonthlyCost != 0)
            {
                ConsoleEx.WriteAt(2, 10, "This decision would", ConsoleColor.Black);
            }

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

            return pressAnyKeyWithYesControl.Show();
        }
    }
}
