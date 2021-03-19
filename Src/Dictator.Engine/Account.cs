using System;

namespace Dictator.Core
{
    public class Account: IAccount
    {
        public int TreasuryBalance { get; set; }
        public int MonthlyCosts { get; set; }
        public int SwissBankAccountBalance { get; set; }
        public bool HasSwissBankAccount { get; set; }

        public Account()
        {
            this.Initialise();
        }

        public void Initialise()
        {
            this.TreasuryBalance = 1000;
            this.MonthlyCosts = 60;
            this.SwissBankAccountBalance = 0;
            this.HasSwissBankAccount = false;
        }

        public void ApplyTreasuryChanges(int cost, int monthlyCost)
        {
            throw new NotImplementedException();
        }
    }
}