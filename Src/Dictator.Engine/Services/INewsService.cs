namespace Dictator.Core.Services
{
    public interface INewsService
    {
        public void Initialise();
        public News[] GetUnusedNews();
        public bool ShouldNewsHappen();
        public bool DoesUnusedNewsExist();
        public News SelectRandomUnusedNews();
    }
}