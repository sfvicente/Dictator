using System;

namespace Dictator.Core
{
    public class Account: IAccount
    {
        public int TreasuryBalance { get; set; }
        public int MonthlyCosts { get; set; }
        public int SwissBankAccountBalance { get; set; }

        public Account()
        {
            this.Initialise();
        }

        public void Initialise()
        {
            this.TreasuryBalance = 1000;
            this.MonthlyCosts = 60;
            this.SwissBankAccountBalance = 0;
        }
    }
}