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
        /// <returns>The account information including Swiss bank account information, treasury balance
        /// and monthly costs</returns>
        public Account GetAccount();

        /// <summary>
        ///     Retrieves the current treasury balance.
        /// </summary>
        /// <returns></returns>
        public int GetTreasuryBalance();

        /// <summary>
        ///     Determines if the government is bankrupt, which means that the treasury contains no funds.
        /// </summary>
        /// <returns><c>true</c> if government is bankrupt; otherwise, <c>false</c>.</returns>
        public bool IsGovernmentBankrupt();

        /// <summary>
        ///     Retrieves the current monthly costs for the treasury.
        /// </summary>
        /// <returns>The amount in dollars of the treasurey monthly costs.</returns>
        public int GetMonthlyCosts();

        /// <summary>
        ///     Takes the required funds from treasury to pay for the monthly expenses.
        /// </summary>
        public void PayMonthlyCosts();

        /// <summary>
        ///     Removes the specified amount of funds from the treasury.
        /// </summary>
        /// <param name="amount">The amount to spend from the treasury.</param>
        public void PayFromTreasury(int amount);

        /// <summary>
        ///     Modifies both the current treasury balance and monthly costs.
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="monthlyCost"></param>
        public void ApplyTreasuryChanges(int cost, int monthlyCost);

        /// <summary>
        ///     Modifies the treasure current balance.
        /// </summary>
        /// <param name="amount">The amount to add or remove from the treasury.</param>
        public void ChangeTreasuryBalance(int amount);

        /// <summary>
        ///     Gets the current balance in the Swiss bank account.
        /// </summary>
        /// <returns>The amount of dollars currently in the Swiss bank account.</returns>
        public int GetSwissBankAccountBalance();

        /// <summary>
        ///     Deposits the specified amount into the Swiss bank account.
        /// </summary>
        /// <param name="amount"></param>
        public void DepositToSwissBankAccount(int amount);

        /// <summary>
        ///     Steals half of the current treasury funds to the players private Swiss bank account.
        /// </summary>
        /// <returns>The details of the bank transfer consisting of the previous treasury balance and the amount stolen.</returns>
        public SwissBankAccountTransfer TransferToSwissBankAccount();

        /// <summary>
        ///     Applies the bankruptcy state effects which consists of a decrease in popularity with the Army and Secret Police and also decrease the strength
        ///     of the Player and the Secret Police.
        /// </summary>
        public void ApplyBankruptcyEffects();
    }
}