using Dictator.Core;

namespace Dictator.ConsoleInterface.Reporting
{
    /// <summary>
    ///     Represents the screen that is displayed when the player is shown the police report.
    /// </summary>
    public interface IPoliceReportScreen
    {
        public void Show(PoliceReport policeReport);
    }
}
