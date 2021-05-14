using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Reporting
{
    public interface IPoliceReportRequestDialog
    {
        public DialogResult Show(PoliceReportRequest policeReportRequest);
    }
}
