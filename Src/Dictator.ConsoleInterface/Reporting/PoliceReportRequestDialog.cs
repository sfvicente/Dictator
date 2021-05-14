using Dictator.Common.Extensions;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using Dictator.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Reporting
{
    public class PoliceReportRequestDialog : IPoliceReportRequestDialog
    {
        private readonly IPressAnyKeyControl pressAnyKeyControl;
        private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

        public PoliceReportRequestDialog(
            IPressAnyKeyControl pressAnyKeyControl,
            IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        {
            this.pressAnyKeyControl = pressAnyKeyControl;
            this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
        }

        public DialogResult Show(PoliceReportRequest policeReportRequest)
        {
            ConsoleEx.Clear(ConsoleColor.Black, ConsoleColor.White);
            ConsoleEx.WriteAt(1, 1, "################################");
            ConsoleEx.WriteAt(1, 3, "     SECRET POLICE REPORT ?     ");

            DialogResult dialogResult;
            
            if (policeReportRequest.HasEnoughBalance && policeReportRequest.HasEnoughPopularityWithPolice && policeReportRequest.HasPoliceEnoughStrength)
            {
                ConsoleEx.WriteAt(1, 12, "         ( costs $1000 )        ");
                dialogResult = pressAnyKeyWithYesControl.Show();
            }
            else
            {
                ConsoleEx.WriteAt(1, 10, "          NOT AVAILABLE         ");

                int screenRow = 12;

                if (!policeReportRequest.HasEnoughPopularityWithPolice)
                {
                    ConsoleEx.WriteAt(1, screenRow++, $"  Your POPULARITY with us is {policeReportRequest.PolicePopularity}  ");
                }

                if (!policeReportRequest.HasPoliceEnoughStrength)
                {
                    ConsoleEx.WriteAt(1, screenRow++, $"      POLICE strength is {policeReportRequest.PoliceStrength}      ");
                }

                if (!policeReportRequest.HasEnoughBalance)
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
