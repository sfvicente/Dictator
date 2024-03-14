namespace Dictator.Core.Configuration;

public  class AccountSettings
{
    public int InitialTreasuryBalance { get; set; }
    public int InitialMonthlyCosts { get; set; }
    public bool HasSwissBankAccount { get; set; }
    public int InitialSwissBankAccountBalance { get; set; }

    public AccountSettings()
    {
        InitialTreasuryBalance = 1000;
        InitialMonthlyCosts = 60;
        HasSwissBankAccount = false;
        InitialSwissBankAccountBalance = 0;
    }
}
