using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    /// <summary>
    ///     Represents the outcome of a loan application.
    /// </summary>
    public class LoanApplicationResult
    {
        /// <summary>
        ///     Gets or sets if the loan application has been accepted by the country. 
        /// </summary>
        public bool IsAccepted { get; set; }

        /// <summary>
        ///     Gets or sets the reason why the loan has been refused by the country.
        /// </summary>
        public LoanApplicationRefusalType RefusalType { get; set; }

        /// <summary>
        ///     Gets or sets the amount of the loan provided by the country when the application has been accepted. 
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        ///     Gets or sets the country to which the loan application is made to. 
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        ///     Gets or sets the name of the group that the loan application is made to. 
        /// </summary>
        public string GroupName { get; set; }
    }
}
