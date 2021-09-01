namespace Dictator.Core.Services
{
    /// <summary>
    ///     Provides functionality related to the request for financial aids.
    /// </summary>
    public interface ILoanService
    {
        /// <summary>
        ///     Asks for foreign aid in the form of a loan to either America or Russia.
        /// </summary>
        /// <param name="country">The country to which the loan request will be made.</param>
        /// <returns>The loan application result that includes if the loan has been approved or refused.</returns>
        public LoanApplicationResult AskForLoan(LenderCountry country);
    }
}
