using Dictator.Core.Configuration;
using Dictator.Core.Models;

namespace Dictator.Core;

public interface IGameState
{
    void Initialise();
    Account GetAccount();
}

public class GameState : IGameState
{
    private readonly IAccountSettings _accountSettings;

    public Account Account { get; set; } = new();

    public GameState()
        : this(new AccountSettings())
    {
    }

    public GameState(IAccountSettings accountSettings)
    {
        _accountSettings = accountSettings;
        Initialise();
    }

    public void Initialise()
    {
        Account.TreasuryBalance = _accountSettings.InitialTreasuryBalance;
        Account.MonthlyCosts = _accountSettings.InitialMonthlyCosts;
        Account.HasSwissBankAccount = _accountSettings.HasSwissBankAccount;
        Account.SwissBankAccountBalance = _accountSettings.InitialSwissBankAccountBalance;
    }

    public Account GetAccount()
    { 
        return Account;
    }
}
