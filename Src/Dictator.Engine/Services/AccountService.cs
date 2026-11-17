using Dictator.Core.Models;

namespace Dictator.Core.Services;

/// <summary>
///     Provides functionality related to the treasury and associated costs and the Swiss bank account.
/// </summary>
public interface IAccountService
{
    /// <summary>
    ///     Retrieves the current account aggregated properties.
    /// </summary>
    /// <returns>The account information including Swiss bank account information, treasury balance
    /// and monthly costs.</returns>
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

/// <summary>
///     Provides functionality related to the treasury and associated costs and the Swiss bank account.
/// </summary>
public class AccountService : IAccountService
{
    private readonly Account _account;
    private readonly IGroupService _groupService;
    private readonly IStatsService _statsService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AccountService"/> class from a <see cref="IGameState"/>,
    ///     a <see cref="IGroupService"/> and a <see cref="IStatsService"/> components.
    /// </summary>
    /// <param name="gameState">The component used to provide information related to the accounts and costs.</param>
    /// <param name="groupService">The service used to provide functionality related to the groups or factions.</param>
    /// <param name="statsService">The service used to provide functionality related to the player and monthly stats.</param>
    public AccountService(
        IGameState gameState,
        IGroupService groupService,
        IStatsService statsService)
    {
        _account = gameState.GetAccount();
        _groupService = groupService;
        _statsService = statsService;
    }

    /// <summary>
    ///     Retrieves the current account aggregated properties.
    /// </summary>
    /// <returns>The account information including Swiss bank account information, treasury balance
    /// and monthly costs.</returns>
    public Account GetAccount()
    {
        return _account;
    }

    /// <summary>
    ///     Gets the current balance of the government treasury.
    /// </summary>
    /// <returns></returns>
    public int GetTreasuryBalance()
    {
        return _account.TreasuryBalance;
    }

    /// <summary>
    ///     Determines if the government is bankrupt, which means that the treasury contains no funds.
    /// </summary>
    /// <returns><c>true</c> if government is bankrupt; otherwise, <c>false</c>.</returns>
    public bool IsGovernmentBankrupt()
    {
        return GetTreasuryBalance() <= 0;
    }

    /// <summary>
    ///     Retrieves the current monthly costs for the treasury.
    /// </summary>
    /// <returns>The amount in dollars of the treasurey monthly costs.</returns>
    public int GetMonthlyCosts()
    {
        return _account.MonthlyCosts;
    }

    /// <summary>
    ///     Takes the required funds from treasury to pay for the monthly expenses.
    /// </summary>
    public void PayMonthlyCosts()
    {
        if (GetTreasuryBalance() > 0)
        {
            ChangeTreasuryBalance(-GetMonthlyCosts());
        }
    }

    /// <summary>
    ///     Removes the specified amount of funds from the treasury.
    /// </summary>
    /// <param name="amount">The amount to spend from the treasury.</param>
    public void PayFromTreasury(int amount)
    {
        ChangeTreasuryBalance(-amount);
    }

    public void ApplyTreasuryChanges(int cost, int monthlyCost)
    {
        _account.TreasuryBalance += 10 * cost;
        _account.MonthlyCosts -= monthlyCost;

        if(_account.MonthlyCosts < 0)
        {
            _account.MonthlyCosts = 0;
        }
    }

    /// <summary>
    ///     Modifies the treasure current balance.
    /// </summary>
    /// <param name="amount">The amount to add or remove from the treasury.</param>
    public void ChangeTreasuryBalance(int amount)
    {
        _account.TreasuryBalance += amount;
    }

    /// <summary>
    ///     Gets the current balance in the Swiss bank account.
    /// </summary>
    /// <returns>The amount of dollars currently in the Swiss bank account.</returns>
    public int GetSwissBankAccountBalance()
    {
        return _account.SwissBankAccountBalance;
    }

    /// <summary>
    ///     Adds the specified amount to the Swiss bank account balance.
    /// </summary>
    /// <param name="amount">The amount to be added to the account.</param>
    public void DepositToSwissBankAccount(int amount)
    {
        _account.SwissBankAccountBalance += amount;
    }

    /// <summary>
    ///     Steals half of the current treasury funds to the players private Swiss bank account.
    /// </summary>
    /// <returns>The details of the bank transfer consisting of the previous treasury balance and the amount stolen.</returns>
    public SwissBankAccountTransfer TransferToSwissBankAccount()
    {
        int treasuryPreviousBalance = GetTreasuryBalance();
        int amountStolen = treasuryPreviousBalance / 2;

        if (amountStolen > 0)
        {
            if (!HasSwissBankAccount())
            {
                OpenSwissBankAccount();
            }

            ChangeTreasuryBalance(-amountStolen);
            DepositToSwissBankAccount(amountStolen);
        }

        var swissBankAccountTransfer = new SwissBankAccountTransfer()
        {
            AmountStolen = amountStolen,
            TreasuryPreviousBalance = treasuryPreviousBalance
        };

        return swissBankAccountTransfer;
    }

    /// <summary>
    ///     Applies the bankruptcy state effects which consists of a decrease in popularity with the Army and Secret Police and also decrease the strength
    ///     of the Player and the Secret Police.
    /// </summary>
    public void ApplyBankruptcyEffects()
    {
        _groupService.DecreasePopularity(GroupType.Army, 1);
        _groupService.DecreasePopularity(GroupType.SecretPolice, 1);
        _groupService.DecreaseStrength(GroupType.SecretPolice);
        _statsService.DecreasePlayerStrength();
    }

    /// <summary>
    ///     Opens a Swiss bank account to allow for transfers to me made from treasury.
    /// </summary>
    private void OpenSwissBankAccount()
    {
        _account.HasSwissBankAccount = true;
    }

    /// <summary>
    ///     Determines if a Swiss account has been opened.
    /// </summary>
    /// <returns></returns>
    private bool HasSwissBankAccount()
    {
        return _account.HasSwissBankAccount;
    }

}