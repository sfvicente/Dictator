using Dictator.ConsoleInterface.Common;
using Dictator.Core;

namespace Dictator.ConsoleInterface.Reporting
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player is asked if a police report should be displayed
    ///     for a specific cost.
    /// </summary>
    public interface IPoliceReportRequestDialog
    {
        public DialogResult Show(PoliceReportRequest policeReportRequest);
    }
}
