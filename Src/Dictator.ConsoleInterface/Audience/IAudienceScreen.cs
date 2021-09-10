namespace Dictator.ConsoleInterface.Audience
{
    /// <summary>
    ///     Represents the screen that is displayed when an audience is requested to the player
    ///     by one of the groups.
    /// </summary>
    public interface IAudienceScreen
    {
        public void Show(Core.Audience audience);
    }
}
