namespace Dictator.ConsoleInterface.Start
{
    /// <summary>
    ///     Represents the screen that is displayed when welcoming the player to the game.
    /// </summary>
    public interface IWelcomeScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="highscore">The game's current highest score.</param>
        public void Show(int highscore);
    }
}