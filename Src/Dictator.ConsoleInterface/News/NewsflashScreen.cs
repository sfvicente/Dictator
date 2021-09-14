using Dictator.Common;
using System;

namespace Dictator.ConsoleInterface.News
{
    /// <summary>
    ///     Represents the screen that is displayed when a random newsflash happens.
    /// </summary>
    public class NewsflashScreen : INewsflashScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="headline">The news headline to be presented on screen.</param>
        public void Show(string headline)
        {
            ConsoleEx.Clear(ConsoleColor.Gray);
            ConsoleEx.WriteAt(1, 10, "NEWSFLASH");
            ConsoleEx.WriteAt(1, 14, headline);
            Console.ReadKey(true);
        }
    }
}
