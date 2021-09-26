namespace Dictator.ConsoleInterface.Start
{
    public interface IWelcomeScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="highscore">The game's current highest score.</param>
        public void Show(int highscore);
    }
}