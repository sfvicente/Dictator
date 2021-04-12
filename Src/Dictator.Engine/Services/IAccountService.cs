using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IAccountService
    {
        public void Initialise();
        public int GetTreasuryBalance();
        public int GetMonthlyCosts();
        public void ApplyTreasuryChanges(int cost, int monthlyCost);
        public void ChangeTreasuryBalance(int amount);

        public bool HasSwissBankAccount();
        public int GetSwissBankAccountBalance();
        public void DepositToSwissBankAccount(int amount);
    }
}
