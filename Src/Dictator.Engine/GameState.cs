using Dictator.Core.Configuration;
using Dictator.Core.Models;

namespace Dictator.Core;

public interface IGameState
{
    Account GetAccount();
}

public class GameState : IGameState
{
    public Account Account { get; set; } = new();

    public GameState(IAccountSettings accountSettings)
    {
        Account.TreasuryBalance = accountSettings.InitialTreasuryBalance;
        Account.MonthlyCosts = accountSettings.InitialMonthlyCosts;
        Account.HasSwissBankAccount = accountSettings.HasSwissBankAccount;
        Account.SwissBankAccountBalance = accountSettings.InitialSwissBankAccountBalance;
    }

    public Account GetAccount()
    { 
        return Account;
    }
}
