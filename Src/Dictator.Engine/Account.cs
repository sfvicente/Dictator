using System;

namespace Dictator.Core
{
    public class Account: IAccount
    {
        public int TreasuryBalance { get; private set; }
        public int MonthlyCosts { get; private set; }
        public int SwissBankAccountBalance { get; private set; }
        public bool HasSwissBankAccount { get; private set; }

        public Account()
        {
            Initialise();
        }

        public void Initialise()
        {
            TreasuryBalance = 1000;
            MonthlyCosts = 60;
            SwissBankAccountBalance = 0;
            HasSwissBankAccount = false;
        }

        public void ApplyTreasuryChanges(int cost, int monthlyCost)
        {
            // TODO: apply changes
        }

        public void OpenSwissBankAccount()
        {
            HasSwissBankAccount = true;
        }

        public void TransferToSwissBankAccount(int amount)
        {
            SwissBankAccountBalance += amount;
        }

        public void ChangeTreasuryBalance(int amount)
        {
            TreasuryBalance += amount;
        }
    }
}