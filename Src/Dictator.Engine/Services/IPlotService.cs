namespace Dictator.Core.Services
{
    public interface IPlotService
    {
        /// <summary>
        ///     Checks for the required conditions and changes que appropriate group status
        ///     to the revolution and assassination modes.
        /// </summary>
        public void Plot();
    }
}
