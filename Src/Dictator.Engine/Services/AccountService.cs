using System;

namespace Dictator.Core.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccount account;

        public AccountService(IAccount account)
        {
            this.account = account;
            Initialise();
        }

        public void Initialise()
        {
            account.TreasuryBalance = 1000;
            account.MonthlyCosts = 60;
            account.SwissBankAccountBalance = 0;
            account.HasSwissBankAccount = false;
        }

        /// <summary>
        ///     Gets the current balance of the government treasury.
        /// </summary>
        /// <returns></returns>
        public int GetTreasuryBalance()
        {
            return account.TreasuryBalance;
        }

        public int GetMonthlyCosts()
        {
            return account.MonthlyCosts;
        }

        public int GetSwissBankAccountBalance()
        {
            return account.SwissBankAccountBalance;
        }

        public bool HasSwissBankAccount()
        {
            return account.HasSwissBankAccount;
        }

        public void ApplyTreasuryChanges(int cost, int monthlyCost)
        {
            account.TreasuryBalance += 10 * cost;
            account.MonthlyCosts -= monthlyCost;

            if(account.MonthlyCosts < 0)
            {
                account.MonthlyCosts = 0;
            }
        }

        public void OpenSwissBankAccount()
        {
            account.HasSwissBankAccount = true;
        }

        public void TransferToSwissBankAccount(int amount)
        {
            account.SwissBankAccountBalance += amount;
        }

        public void ChangeTreasuryBalance(int amount)
        {
            account.TreasuryBalance += amount;
        }
    }
}