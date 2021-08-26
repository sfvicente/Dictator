using System;
using System.ComponentModel;

namespace Dictator.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly IGovernmentService governmentService;
        private readonly IGroupService groupService;
        private readonly IAccountService accountService;

        public LoanService(IGovernmentService governmentService, IGroupService groupService, IAccountService accountService)
        {
            this.governmentService = governmentService;
            this.groupService = groupService;
            this.accountService = accountService;
        }

        /// <summary>
        ///     Asks for foreign aid in the form of a loan to either America or Russia.
        /// </summary>
        /// <param name="country">The country of the lender to which the loan will be requested.</param>
        /// <returns>The loan application result that includes if the loan has been approved or refused.</returns>
        public LoanApplicationResult AskForLoan(LenderCountry country)
        {
            LoanApplicationResult loanApplicationResult = new LoanApplicationResult
            {
                Country = country
            };

            if (IsTooEarlyForLoan())
            {
                loanApplicationResult.IsAccepted = false;
                loanApplicationResult.RefusalType = LoanApplicationRefusalType.TooEarly;

                return loanApplicationResult;
            }

            if(HasLoanBeenGrantedPreviously(country))
            {
                loanApplicationResult.IsAccepted = false;
                loanApplicationResult.RefusalType = LoanApplicationRefusalType.AlreadyUsed;

                return loanApplicationResult;
            }

            GroupType groupType;

            switch (country)
            {
                case LenderCountry.America:
                    groupType = GroupType.Americans;
                    break;

                case LenderCountry.Russia:
                    groupType = GroupType.Russians;
                    break;

                default:
                    throw new InvalidEnumArgumentException(nameof(country), (int)country, country.GetType());
            }

            Group group = groupService.GetGroupByType(groupType);

            loanApplicationResult.GroupName = group.Name;

            if (group.Popularity <= governmentService.GetMonthlyMinimalPopularityAndStrength())
            {
                loanApplicationResult.IsAccepted = false;
                loanApplicationResult.RefusalType = LoanApplicationRefusalType.NotPopularEnough;
            }
            else
            {
                loanApplicationResult.IsAccepted = true;
                loanApplicationResult.RefusalType = LoanApplicationRefusalType.None;
                loanApplicationResult.Amount = CalculateLoanAmount(group);
                accountService.ChangeTreasuryBalance(loanApplicationResult.Amount);
            }

            return loanApplicationResult;
        }

        /// <summary>
        ///     Determines if it is too early in the game to ask for foreign help.
        /// </summary>
        /// <returns><c>true</c> if it is too early in the game to receive a loan; otherwise, <c>false</c>.</returns>
        private bool IsTooEarlyForLoan()
        {
            Random random = new Random();
            int minimumRandomMonthRequirement = random.Next(0, 5) + 3;

            if (governmentService.GetMonth() <= minimumRandomMonthRequirement)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Determines if a loan has already been requested and granted by the specified country.
        /// </summary>
        /// <param name="country">The country of the lender to which the loan will be requested.</param>
        /// <returns><c>true</c> if the loan has been granted before; otherwise, <c>false</c>.</returns>
        private bool HasLoanBeenGrantedPreviously(LenderCountry country)
        {
            // TODO: Check if loans have been used

            return false;
        }

        /// <summary>
        ///     Calculates the amount of a loan based on the popularity of a group and a random component.
        /// </summary>
        /// <param name="group">The group from which the popularity will be used as a component to calculate the amount.</param>
        /// <returns>The calculated amount of the loan.</returns>
        private int CalculateLoanAmount(Group group)
        {
            Random random = new Random();
            int randomAdditionalAmount = random.Next(0, 200);
            int amount = group.Popularity * 30 + randomAdditionalAmount;

            return amount;
        }
    }
}
