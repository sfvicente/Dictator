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
    private readonly IRandomService _randomService;
    private readonly IStateManagementService _stateManagementService;
    private readonly IGroupService _groupService;
    private readonly IStatsService _statsService;
    private readonly IGovernmentService _governmentService;
    private readonly IAccountService _accountService;

    public LoanService(
        IRandomService randomService,
        IStateManagementService stateManagementService,
        IAccountService accountService,
        IGroupService groupService,
        IStatsService statsService,
        IGovernmentService governmentService)
    {
        _governmentService = governmentService;
        _groupService = groupService;
        _statsService = statsService;
        _randomService = randomService;
        _stateManagementService = stateManagementService;
        _accountService = accountService;
    }

    /// <summary>
    ///     Asks for foreign aid in the form of a loan to either America or Russia.
    /// </summary>
    /// <param name="lenderCountry">The country to which the loan request will be made.</param>
    /// <returns>The loan application result that includes if the loan has been approved or refused.</returns>
    public LoanApplicationResult AskForLoan(LenderCountry lenderCountry)
    {
        LoanApplicationResult loanApplicationResult = new() { Country = lenderCountry };

        if (IsTooEarlyForLoan())
        {
            loanApplicationResult.IsAccepted = false;
            loanApplicationResult.RefusalType = LoanApplicationRefusalType.TooEarly;

            return loanApplicationResult;
        }

        if (_stateManagementService.HasLoanBeenGranted(lenderCountry))
        {
            loanApplicationResult.IsAccepted = false;
            loanApplicationResult.RefusalType = LoanApplicationRefusalType.AlreadyUsed;

            return loanApplicationResult;
        }

        GroupType groupType = _groupService.GetGroupTypeByCountry(lenderCountry);
        Group group = _groupService.GetGroupByType(groupType);

        loanApplicationResult.GroupName = group.Name;

        if (group.Popularity <= _statsService.GetMonthlyMinimalPopularityAndStrength())
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
            _stateManagementService.SetLoanHasBeenGranted(lenderCountry);
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

        if (_governmentService.GetMonth() < minimumRandomMonthRequirement)
        {
            return true;
        }

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
