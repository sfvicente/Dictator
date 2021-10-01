namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when a player loses the war, which results
    ///     in a Leftotan victory.
    /// </summary>
    public interface IWarLostScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show();
    }
}
