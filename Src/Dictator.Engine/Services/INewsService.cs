namespace Dictator.Core.Services
{
    public interface INewsService
    {
        public void Initialise();
        public News[] GetUnusedNews();

        /// <summary>
        ///     Determines if a random news event should happen in the current month.
        /// </summary>
        /// <returns><c>true</c> if a random news event should happen in the current month; otherwise, <c>false</c>.</returns>
        public bool ShouldNewsHappen();

        public bool DoesUnusedNewsExist();
        public News SelectRandomUnusedNews();
    }
}