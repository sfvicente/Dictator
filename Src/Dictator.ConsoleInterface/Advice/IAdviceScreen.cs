using Dictator.Core;

namespace Dictator.ConsoleInterface.Advice
{
    public interface IAdviceScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="gameAction">The game action to be displayed on the screen.</param>
        public void Show(GameAction gameAction);
    }
}
