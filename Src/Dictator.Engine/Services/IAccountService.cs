using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IAccountService
    {
        /// <summary>
        ///     Initialises the account data.
        /// </summary>
        public void Initialise();

        /// <summary>
        ///     Retrieves the current account aggregated properties.
        /// </summary>
        /// <returns></returns>
        public Account GetAccount();

        /// <summary>
        ///     Retrieves the current treasury balance.
        /// </summary>
        /// <returns></returns>
        public int GetTreasuryBalance();

        /// <summary>
        ///     Retrives the current monthly costs for the treasury.
        /// </summary>
        /// <returns></returns>
        public int GetMonthlyCosts();

        /// <summary>
        ///     Takes the required funds from treasury to pay for the monthly expenses.
        /// </summary>
        public void PayMonthlyCosts();

        /// <summary>
        ///     Modifies both the current treasury balance and monthly costs.
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="monthlyCost"></param>
        public void ApplyTreasuryChanges(int cost, int monthlyCost);

        /// <summary>
        ///     Modifies the treasure current balance.
        /// </summary>
        /// <param name="amount"></param>
        public void ChangeTreasuryBalance(int amount);

        /// <summary>
        ///     Determines if a Swiss account has been opened.
        /// </summary>
        /// <returns></returns>
        public bool HasSwissBankAccount();

        /// <summary>
        ///     Opens a Swiss bank account to allow for transfers to me made from treasury.
        /// </summary>
        public void OpenSwissBankAccount();

        /// <summary>
        ///     Gets the current balance in the Swiss bank account.
        /// </summary>
        /// <returns></returns>
        public int GetSwissBankAccountBalance();

        /// <summary>
        ///     Deposits the specified amount into the Swiss bank account.
        /// </summary>
        /// <param name="amount"></param>
        public void DepositToSwissBankAccount(int amount);

        public SwissBankAccountTransfer TransferToSwissBankAccount();
    }
}