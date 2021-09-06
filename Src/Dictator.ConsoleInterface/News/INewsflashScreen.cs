namespace Dictator.ConsoleInterface.News
{
    public interface INewsflashScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="headline">The news headline to be presented on screen.</param>
        public void Show(string headline);
    }
}