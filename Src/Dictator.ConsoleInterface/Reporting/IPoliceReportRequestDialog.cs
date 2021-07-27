using Dictator.ConsoleInterface.Common;
using Dictator.Core;

namespace Dictator.ConsoleInterface.Reporting
{
    public interface IPoliceReportRequestDialog
    {
        public DialogResult Show(PoliceReportRequest policeReportRequest);
    }
}
