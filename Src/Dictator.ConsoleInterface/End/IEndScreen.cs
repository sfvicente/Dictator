using Dictator.Core;

namespace Dictator.ConsoleInterface.End
{
    public interface IEndScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="score">The game score to display on the screen.</param>
        public void Show(Score score);
    }
}
