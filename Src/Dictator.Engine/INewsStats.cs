namespace Dictator.Core
{
    public interface INewsStats
    {
        public News CurrentNews { get; set; }

        public void Initialise();
        public News[] GetNews();
    }
}