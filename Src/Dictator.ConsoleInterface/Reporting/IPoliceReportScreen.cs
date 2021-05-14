using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Reporting
{
    public interface IPoliceReportScreen
    {
        public void Show(PoliceReport policeReport);
    }
}
