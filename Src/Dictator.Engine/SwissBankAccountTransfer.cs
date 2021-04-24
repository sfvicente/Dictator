using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    /// <summary>
    ///     Represents a bank transfer from the treasury to the Swiss bank account.
    /// </summary>
    public class SwissBankAccountTransfer
    {
        /// <summary>
        ///     Gets or sets the amount that has been stolen from the treasury and transfered to the Swiss bank account.
        /// </summary>
        public int AmountStolen { get; set; }

        /// <summary>
        ///     Gets or sets the amount that was in the treasury prior to the transfer to the Swiss bank account.
        /// </summary>
        public int TreasuryPreviousBalance { get; set; }
    }
}
