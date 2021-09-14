namespace Dictator.ConsoleInterface.News
{
    /// <summary>
    ///     Represents the screen that is displayed when a random newsflash happens.
    /// </summary>
    public interface INewsflashScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="headline">The news headline to be presented on screen.</param>
        public void Show(string headline);
    }
}