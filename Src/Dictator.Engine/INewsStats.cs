namespace Dictator.Core
{
    public interface INewsStats
    {
        public void Initialise();
        public News[] GetNews();
    }
}