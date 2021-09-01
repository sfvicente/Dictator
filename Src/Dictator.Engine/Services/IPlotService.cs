namespace Dictator.Core.Services
{
    /// <summary>
    ///     Provides functionality related to changes in status of groups to initiate assassinations or revolutions.
    /// </summary>
    public interface IPlotService
    {
        /// <summary>
        ///     Checks for the required conditions and changes que appropriate group status
        ///     to the revolution and assassination modes.
        /// </summary>
        public void Plot();
    }
}
