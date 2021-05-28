namespace Dictator.Core.Services
{
    public interface INewsService
    {
        public void Initialise();
        public News[] GetUnusedNews();
        public bool ShouldNewsHappen();
        public News SelectRandomUnusedNews();
    }
}