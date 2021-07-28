namespace Dictator.Core
{
    public class Account : IAccount
    {
        /// <summary>
        ///     Gets or sets the balance of the treasury.
        /// </summary>
        public int TreasuryBalance { get; set; }

        /// <summary>
        ///     Gets or sets the monthly costs for the treasury.
        /// </summary>
        public int MonthlyCosts { get; set; }

        /// <summary>
        ///     Gets or sets a value that indicates whether the player has a Swiss bank account.
        /// </summary>
        public bool HasSwissBankAccount { get; set; }

        /// <summary>
        ///     Gets or sets the balance of the Swiss bank account.
        /// </summary>
        public int SwissBankAccountBalance { get; set; }
    }
}
