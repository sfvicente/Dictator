using Dictator.Core.Configuration;

namespace Dictator.Core;

public interface IGameState
{

}

public class GameState : IGameState
{
    public int TreasuryBalance { get; set; }
    public int MonthlyCosts { get; set; }
    public bool HasSwissBankAccount { get; set; }
    public int SwissBankAccountBalance { get; set; }

    public GameState(AccountSettings accountSettings)
    {
        TreasuryBalance = accountSettings.InitialTreasuryBalance;
        MonthlyCosts = accountSettings.InitialMonthlyCosts;
        HasSwissBankAccount = accountSettings.HasSwissBankAccount;
        SwissBankAccountBalance = accountSettings.InitialSwissBankAccountBalance;
    }

}
