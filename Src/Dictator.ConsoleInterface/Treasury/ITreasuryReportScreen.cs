using Dictator.Core;

namespace Dictator.ConsoleInterface.Treasury
{
    /// <summary>
    ///     Represents the screen that is displayed when a report of the treasury is required.
    /// </summary>
    public interface ITreasuryReportScreen
    {
        public void Show(Account account);
    }
}
