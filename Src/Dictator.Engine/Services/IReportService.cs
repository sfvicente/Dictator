using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IReportService
    {
        public PoliceReportRequest RequestPoliceReport();
        public PoliceReport GetPoliceReport();
    }
}
