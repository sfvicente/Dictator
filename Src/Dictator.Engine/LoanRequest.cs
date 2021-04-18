using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class LoanRequest
    {
        public bool IsAccepted { get; set; }
        public int Amount { get; set; }
        public Country Country { get; set; }
    }
}
