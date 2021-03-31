using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IAccount
    {
        public int TreasuryBalance { get; set; }
        public int MonthlyCosts { get; set; }
        public bool HasSwissBankAccount { get; set; }
        public int SwissBankAccountBalance { get; set; }
    }
}
