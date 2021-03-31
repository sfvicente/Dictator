﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IAccountService
    {
        public void Initialise();
        public int GetTreasuryBalance();
        public int GetMonthlyCosts();
        public int GetSwissBankAccountBalance();
        public bool HasSwissBankAccount();
        public void ApplyTreasuryChanges(int cost, int monthlyCost);
        public void ChangeTreasuryBalance(int amount);
    }
}
