namespace Dictator.Core.Models
{
    /// <summary>
    ///     Represents the account information including Swiss bank account information, treasury balance
    ///     and monthly costs.
    /// </summary>
    public class Account
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
