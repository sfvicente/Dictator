namespace Dictator.ConsoleInterface.Escape
{
    /// <summary>
    ///     Represents the screen that is displayed when the player attempts escape but the helicopter
    ///     engine fails.
    /// </summary>
    public interface IHelicopterEngineFailureScreen
    {
        /// <summary>
        ///     Displays the screen.
        /// </summary>
        public void Show();
    }
}
