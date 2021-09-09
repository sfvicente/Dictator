using Dictator.Core;

namespace Dictator.ConsoleInterface.End
{
    /// <summary>
    ///     Represents the screen that is displayed when an instance of a game ends. It displays
    ///     the partial score components and final game score.
    /// </summary>
    public interface IEndScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="score">The game score to display on the screen.</param>
        public void Show(Score score);
    }
}
