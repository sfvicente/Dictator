namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the screen that is displayed when the player does not have enough popularity
    ///     with a group for them to accept being their ally in a revolution.
    /// </summary>
    public interface IRevolutionAllyLowPopularityScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show();
    }
}
