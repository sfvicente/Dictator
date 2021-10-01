namespace Dictator.ConsoleInterface.War
{
    /// <summary>
    ///     Represents the screen that is displayed when a player loses the war, is unable to
    ///     escape by helicopter and is executed.
    /// </summary>
    public interface IWarExecutionScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show();
    }
}
