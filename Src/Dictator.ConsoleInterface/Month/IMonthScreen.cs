namespace Dictator.ConsoleInterface.Month
{
    public interface IMonthScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="month">The number of the month to display on the screen.</param>
        public void Show(int month);
    }
}
