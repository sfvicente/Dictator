using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using Dictator.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PoliceReport
{
    public class PoliceReportRequestDialog : IPoliceReportRequestDialog
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;
        private readonly IGroupStats groupStats;

        public PoliceReportRequestDialog(
            IPressAnyKeyControl pressAnyKeyControl,
            IPressAnyKeyWithYesControl pressAnyKeyWithYesControl,
            IGroupStats groupStats)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
            this.groupStats = groupStats;
        }

        public DialogResult Show(bool hasEnoughBalance, bool hasEnoughPopularityWithPolice, bool hasPoliceEnoughStrength)
        {
            ConsoleEx.Clear(ConsoleColor.Black, ConsoleColor.White);
            ConsoleEx.WriteAt(1, 1, "################################");
            ConsoleEx.WriteAt(1, 3, "     SECRET POLICE REPORT ?     ");

            DialogResult dialogResult;
            
            if (hasEnoughBalance && hasEnoughPopularityWithPolice && hasPoliceEnoughStrength)
            {
                ConsoleEx.WriteAt(1, 12, "         ( costs $1000 )        ");
                dialogResult = pressAnyKeyWithYesControl.Show();
            }
            else
            {
                ConsoleEx.WriteAt(1, 10, "          NOT AVAILABLE         ");

                int screenRow = 12;

                if (!hasEnoughPopularityWithPolice)
                {
                    ConsoleEx.WriteAt(1, screenRow++, $"  Your POPULARITY with us is {groupStats.PoliceStrength}  ");
                }

                if (!hasPoliceEnoughStrength)
                {
                    ConsoleEx.WriteAt(1, screenRow++, $"      POLICE strength is {groupStats.PoliceStrength}      ");
                }

                if (!hasEnoughBalance)
                {
                    ConsoleEx.WriteAt(1, screenRow++, "    You can't AFFORD a REPORT    ");
                }

                dialogResult = DialogResult.No;
                pressAnyKeyControl.Show();
            }

            return dialogResult;
        }
    }
}
