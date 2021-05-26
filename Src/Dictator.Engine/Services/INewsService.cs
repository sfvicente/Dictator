namespace Dictator.Core.Services
{
    public interface INewsService
    {
        public void Initialise();
        public News[] GetNews();
        public News[] GetUnusedNews();
        public bool ShouldNewsHappen();
        public void MarkNewsAsUsed(string text);
        public News SelectRandomUnusedNews();
    }
}