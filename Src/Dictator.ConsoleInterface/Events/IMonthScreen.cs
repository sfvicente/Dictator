namespace Dictator.ConsoleInterface.Events
{
    /// <summary>
    ///     Represents the screen that is displayed when a new month starts, which
    ///     marks a new game turn.
    /// </summary>
    public interface IMonthScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="month">The number of the month to display on the screen.</param>
        public void Show(int month);
    }
}
