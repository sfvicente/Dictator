using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IReportService
    {
        public PoliceReportRequest RequestPoliceReport();

        /// <summary>
        ///     Retrieves the police report with the current month, revolution strength, player strength and group information.
        /// </summary>
        /// <returns>The police report with the current month, revolution strength, player strength and group information.</returns>
        public PoliceReport GetPoliceReport();
    }
}
