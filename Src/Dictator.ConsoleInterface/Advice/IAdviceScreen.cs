using Dictator.Core;

namespace Dictator.ConsoleInterface.Advice
{
    /// <summary>
    ///     Represents the screen that is displayed when a player is given advice on the impacts of
    ///     a game action on the groups.
    /// </summary>
    public interface IAdviceScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        /// <param name="gameAction">The game action to be displayed on the screen.</param>
        public void Show(GameAction gameAction);
    }
}
