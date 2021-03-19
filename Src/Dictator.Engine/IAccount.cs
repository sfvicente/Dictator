using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IAccount
    {
        public int TreasuryBalance { get; }
        public int MonthlyCosts { get; }
        public bool HasSwissBankAccount { get; }
        public int SwissBankAccountBalance { get; }

        public void Initialise();
        public void ApplyTreasuryChanges(int cost, int monthlyCost);
        public void ChangeTreasuryBalance(int amount);
    }
}
