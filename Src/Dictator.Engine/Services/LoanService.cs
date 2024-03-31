using Dictator.Core.Models;

namespace Dictator.Core.Services;

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

/// <summary>
///     Provides functionality related to the request for financial aids.
/// </summary>
public class LoanService : ILoanService
{
    private readonly IGovernmentService _governmentService;
    private readonly IGroupService _groupService;
    private readonly IPopularityService _popularityService;
    private readonly IRandomService _randomService;
    private readonly IAccountService _accountService;

    public LoanService(
        IRandomService randomService,
        IAccountService accountService,
        IGroupService groupService,
        IPopularityService popularityService,
        IGovernmentService governmentService)
    {
        _governmentService = governmentService;
        _groupService = groupService;
        _popularityService = popularityService;
        _randomService = randomService;
        _accountService = accountService;
    }

    /// <summary>
    ///     Asks for foreign aid in the form of a loan to either America or Russia.
    /// </summary>
    /// <param name="country">The country to which the loan request will be made.</param>
    /// <returns>The loan application result that includes if the loan has been approved or refused.</returns>
    public LoanApplicationResult AskForLoan(LenderCountry country)
    {
        LoanApplicationResult loanApplicationResult = new()
        {
            Country = country
        };

        if (IsTooEarlyForLoan())
        {
            loanApplicationResult.IsAccepted = false;
            loanApplicationResult.RefusalType = LoanApplicationRefusalType.TooEarly;

            return loanApplicationResult;
        }

        if (HasLoanBeenGrantedPreviously(country))
        {
            loanApplicationResult.IsAccepted = false;
            loanApplicationResult.RefusalType = LoanApplicationRefusalType.AlreadyUsed;

            return loanApplicationResult;
        }

        GroupType groupType = _groupService.GetGroupTypeByCountry(country);
        Group group = _groupService.GetGroupByType(groupType);

        loanApplicationResult.GroupName = group.Name;

        if (group.Popularity <= _popularityService.GetMonthlyMinimalPopularityAndStrength())
        {
            loanApplicationResult.IsAccepted = false;
            loanApplicationResult.RefusalType = LoanApplicationRefusalType.NotPopularEnough;
        }
        else
        {
            loanApplicationResult.IsAccepted = true;
            loanApplicationResult.RefusalType = LoanApplicationRefusalType.None;
            loanApplicationResult.Amount = CalculateLoanAmount(group);
            _accountService.ChangeTreasuryBalance(loanApplicationResult.Amount);
        }

        return loanApplicationResult;
    }

    /// <summary>
    ///     Determines if it is too early in the game to ask for foreign help.
    /// </summary>
    /// <returns><c>true</c> if it is too early in the game to receive a loan; otherwise, <c>false</c>.</returns>
    private bool IsTooEarlyForLoan()
    {
        int minimumRandomMonthRequirement = _randomService.Next(0, 5) + 3;      

        if (_governmentService.GetMonth() <= minimumRandomMonthRequirement)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Determines if a loan has already been requested and granted by the specified country.
    /// </summary>
    /// <param name="country">The country to which the loan request will be made.</param>
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
        int randomAdditionalAmount = _randomService.Next(0, 200);
        int amount = group.Popularity * 30 + randomAdditionalAmount;

        return amount;
    }
}
