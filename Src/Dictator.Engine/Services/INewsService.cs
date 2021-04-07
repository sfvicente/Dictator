namespace Dictator.Core.Services
{
    public interface INewsService
    {
        public void Initialise();
        public News[] GetNews();
        public void MarkNewsAsUsed(string text);
    }
}