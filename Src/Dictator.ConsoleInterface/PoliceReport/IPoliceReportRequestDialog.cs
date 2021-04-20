using Dictator.ConsoleInterface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PoliceReport
{
    public interface IPoliceReportRequestDialog
    {
        public DialogResult Show(bool hasEnoughBalance, bool hasEnoughPopularityWithPolice, bool hasPoliceEnoughStrength);
    }
}
