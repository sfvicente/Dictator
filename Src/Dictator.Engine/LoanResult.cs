using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class LoanResult
    {
        public bool IsAccepted { get; set; }
        public int Amount { get; set; }
        public Country Country { get; set; }
    }
}
