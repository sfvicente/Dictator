namespace Dictator.Core.Services
{
    public interface INewsService
    {
        public void Initialise();

        /// <summary>
        ///     Determines if a random news event should happen in the current month.
        /// </summary>
        /// <returns><c>true</c> if a random news event should happen in the current month; otherwise, <c>false</c>.</returns>
        public bool ShouldNewsHappen();

        /// <summary>
        ///     Determines if at least one news event exists that hasn't been used in the current game.
        /// </summary>
        /// <returns><c>true</c> if an unused news event exists; otherwise, <c>false</c>.</returns>
        public bool DoesUnusedNewsExist();

        /// <summary>
        ///     Selects a random news event that hasn't been used in the current game.
        /// </summary>
        /// <returns>A random unused news event.</returns>
        public News SelectRandomUnusedNews();

        /// <summary>
        ///     Applies the effects of a specific news event on the groups and treasury-
        /// </summary>
        /// <param name="news">The news whose effect will be applied.</param>
        public void ApplyNewsEffects(News news);
    }
}