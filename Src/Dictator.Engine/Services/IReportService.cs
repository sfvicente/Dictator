namespace Dictator.Core.Services
{
    public interface IReportService
    {
        /// <summary>
        ///     Retrieves a request for a detailed account or statement from police on the state of the groups and government.
        /// </summary>
        /// <returns>A police report request with information on the popularity and strength requirements.</returns>
        public PoliceReportRequest RequestPoliceReport();

        /// <summary>
        ///     Retrieves the police report with the current month, revolution strength, player strength and group information.
        /// </summary>
        /// <returns>The police report with the current month, revolution strength, player strength and group information.</returns>
        public PoliceReport GetPoliceReport();
    }
}
