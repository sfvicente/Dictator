using Dictator.Common;
using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;

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
            ConsoleEx.WriteEmptyLineAt(1, ConsoleColor.Yellow);
            ConsoleEx.WriteEmptyLineAt(2, ConsoleColor.Yellow);
            ConsoleEx.WriteAt(1, 7, "     SECRET POLICE REPORT ?     ");

            DialogResult dialogResult;
            
            if (policeReportRequest.HasEnoughBalance && policeReportRequest.IsPlayerPopularWithSecretPolice && policeReportRequest.HasPoliceEnoughStrength)
            {
                ConsoleEx.WriteAt(1, 13, "         ( costs $1000 )        ");
                dialogResult = pressAnyKeyWithYesControl.Show();
            }
            else
            {
                ConsoleEx.WriteAt(1, 10, "          NOT AVAILABLE         ");

                int screenRow = 12;

                if (!policeReportRequest.IsPlayerPopularWithSecretPolice)
                {
                    ConsoleEx.WriteAt(1, screenRow++, $"  Your POPULARITY with us is {policeReportRequest.PolicePopularity}  ");
                }

                if (!policeReportRequest.HasPoliceEnoughStrength)
                {
                    ConsoleEx.WriteAt(1, screenRow++, $"      POLICE strength is {policeReportRequest.PoliceStrength}      ");
                }

                if (!policeReportRequest.HasEnoughBalance)
                {
                    ConsoleEx.WriteAt(1, screenRow++, "    You can't AFFORD a REPORT   ");
                }

                dialogResult = DialogResult.No;
                pressAnyKeyControl.Show();
            }

            return dialogResult;
        }
    }
}
