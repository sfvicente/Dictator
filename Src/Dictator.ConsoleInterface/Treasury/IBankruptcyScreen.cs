namespace Dictator.ConsoleInterface.Treasury
{
    /// <summary>
    ///     Represents the screen that is displayed when the treasury is bankrupt, leading to a drop in
    ///     the player's popularity.
    /// </summary>
    public interface IBankruptcyScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show();
    }
}
