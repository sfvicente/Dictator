namespace Dictator.Core
{
    public interface INewsStats
    {
        public News CurrentNews { get; }

        public void Initialise();
        public News[] GetNews();
        public void MarkNewsAsUsed(string text);
        public void SetCurrentNews(News news);
    }
}