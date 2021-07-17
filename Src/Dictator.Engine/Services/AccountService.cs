using System;

namespace Dictator.Core.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccount account;
        private readonly IGroupService groupService;
        private readonly IGovernmentService governmentService;

        public AccountService(IAccount account, IGroupService groupService, IGovernmentService governmentService)
        {
            this.account = account;
            this.groupService = groupService;
            this.governmentService = governmentService;
            Initialise();
        }

        public void Initialise()
        {
            account.TreasuryBalance = 1000;
            account.MonthlyCosts = 60;
            account.SwissBankAccountBalance = 0;
            account.HasSwissBankAccount = false;
        }

        public Account GetAccount()
        {
            return (Account)account;
        }

        /// <summary>
        ///     Gets the current balance of the government treasury.
        /// </summary>
        /// <returns></returns>
        public int GetTreasuryBalance()
        {
            return account.TreasuryBalance;
        }

        /// <summary>
        ///     Determines if the government is bankrupt, which means that the treasury contains no funds.
        /// </summary>
        /// <returns><c>true</c> if government is bankrupt; otherwise, <c>false</c>.</returns>
        public bool IsGovernmentBankrupt()
        {
            return GetTreasuryBalance() <= 0;
        }

        public int GetMonthlyCosts()
        {
            return account.MonthlyCosts;
        }

        /// <summary>
        ///     Takes the required funds from treasury to pay for the monthly expenses.
        /// </summary>
        public void PayMonthlyCosts()
        {
            if (GetTreasuryBalance() > 0)
            {
                ChangeTreasuryBalance(-GetMonthlyCosts());
            }
        }

        public void PayFromTreasury(int amount)
        {
            ChangeTreasuryBalance(-amount);
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

        public void TransferToSwissBankAccount(int amount)
        {
            account.SwissBankAccountBalance += amount;
        }

        public void ChangeTreasuryBalance(int amount)
        {
            account.TreasuryBalance += amount;
        }

        public bool HasSwissBankAccount()
        {
            return account.HasSwissBankAccount;
        }

        public int GetSwissBankAccountBalance()
        {
            return account.SwissBankAccountBalance;
        }

        /// <summary>
        ///     Adds the specified amount to the Swiss bank account balance.
        /// </summary>
        /// <param name="amount">The amount to be added to the account.</param>
        public void DepositToSwissBankAccount(int amount)
        {
            account.SwissBankAccountBalance += amount;
        }

        /// <summary>
        ///     Steals half of the current treasury funds to the players private Swiss bank account.
        /// </summary>
        /// <returns>The details of the bank transfer consisting of the previous treasury balance and the amount stolen.</returns>
        public SwissBankAccountTransfer TransferToSwissBankAccount()
        {
            int treasuryPreviousBalance = GetTreasuryBalance();
            int amountStolen = treasuryPreviousBalance / 2;

            if (amountStolen > 0)
            {
                if (!HasSwissBankAccount())
                {
                    OpenSwissBankAccount();
                }

                ChangeTreasuryBalance(-amountStolen);
                DepositToSwissBankAccount(amountStolen);
            }

            var swissBankAccountTransfer = new SwissBankAccountTransfer()
            {
                AmountStolen = amountStolen,
                TreasuryPreviousBalance = treasuryPreviousBalance
            };

            return swissBankAccountTransfer;
        }

        /// <summary>
        ///     Applies the bankruptcy state effects which consists of a decrease in popularity with the Army and Secret Police and also decrease the strength
        ///     of the Player and the Secret Police.
        /// </summary>
        public void ApplyBankruptcyEffects()
        {
            groupService.DecreasePopularity(GroupType.Army, 1);
            groupService.DecreasePopularity(GroupType.SecretPolice, 1);
            groupService.DecreaseStrength(GroupType.SecretPolice);
            governmentService.DecreasePlayerStrength();
        }

        /// <summary>
        ///     Opens a Swiss bank account to allow for transfers to me made from treasury.
        /// </summary>
        private void OpenSwissBankAccount()
        {
            account.HasSwissBankAccount = true;
        }

    }
}