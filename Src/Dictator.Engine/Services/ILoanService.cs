using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface ILoanService
    {
        /// <summary>
        ///     Asks for foreign aid in the form of a loan to either America or Russia.
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public LoanApplicationResult AskForLoan(Country country);
    }
}
