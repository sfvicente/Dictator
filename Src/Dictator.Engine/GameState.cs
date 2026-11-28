using Dictator.Core.Configuration;
using Dictator.Core.Models;

namespace Dictator.Core;

public interface IGameState
{
    void Initialise();
    Account GetAccount();
    Audience[] GetAudiences();
    Decision[] GetDecisions();
    News[] GetNews();
}

public class GameState : IGameState
{
    private readonly IAccountSettings _accountSettings;

    public Account Account { get; set; } = new();
    public Audience[] Audiences { get; set; } = [];
    public Decision[] Decisions { get; set; } = [];
    public News[] News { get; set; } = [];

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
        LoadAudienceData();
        LoadDecisionsData();
        LoadNewsData();
    }

    public Account GetAccount()
    {
        return Account;
    }

    public Audience[] GetAudiences()
    {
        return Audiences;
    }

    public Decision[] GetDecisions()
    {
        return Decisions;
    }

    public News[] GetNews()
    {
        return News;
    }

    private void LoadAudienceData()
    {
        Audiences =
        [
            new Audience(GroupType.Army, 0, -5/*H*/, "QJLMMMMM", "PKLMMM", "     INTRODUCE CONSCRIPTION     "),
            new Audience(GroupType.Army, 0, 0, "PMJMMMMM", "NMLMMM", " REQUISITION LAND for TRAINING  "),
            new Audience(GroupType.Army, -10/*C*/, 0, "PLNMLMLM", "NMNIMM", "   ATTACK ALL GUERILLA BASES    "),
            new Audience(GroupType.Army, -8/*E*/, 0, "PLMMIMLM", "NMNKMM", "ATTACK GUERILLA BASES in LEFTOTO"),
            new Audience(GroupType.Army, 0, 0, "QONMMIMM", "NMNMMJ", "  SACK the SECRET POLICE CHIEF  "),
            new Audience(GroupType.Army, 0, 0, "PMMMLMIO", "MMMMMM", "EXPEL RUSSIAN MILITARY ADVISORS "),
            new Audience(GroupType.Army, 0, -9/*D*/, "QMLMMMMM", "OLLLMM", " INCREASE the PAY of the TROOPS "),
            new Audience(GroupType.Army, -12/*A*/, 0, "QLLMLLMM", "PLLKLM", "  BUY MORE ARMS and AMMUNITION  "),
            new Audience(GroupType.Peasants, 0, 3/*P*/, "LONMMMMM", "LMMLMM", "   STOP ARMY SIGN-UP COERCION   "),
            new Audience(GroupType.Peasants, 0, 0, "MQIMNMMM", "MOLMMM", "INCREASE the BASIC MINIMUM WAGE "),
            new Audience(GroupType.Peasants, 0, 0, "NQOMMIMM", "NNNNMJ", " CUT the POWERS of the S.POLICE "),
            new Audience(GroupType.Peasants, 0, 0, "MPKMKMMM", "MOKMMM", "STOP LEFTOTAN IMMIGRANT WORKERS "),
            new Audience(GroupType.Peasants, -10/*C*/, -8/*E*/, "LQKMOLNM", "MNLLMM", "INTRODUCE FREE EDUCATION for ALL"),
            new Audience(GroupType.Peasants, 0, 0, "MQJMNLNM", "MPJMML", "LEGALISE the FORMATION of UNIONS"),
            new Audience(GroupType.Peasants, 0, 0, "LQKMNLMM", "MOLLMM", "  FREE their IMPRISONED LEADER  "),
            new Audience(GroupType.Peasants, 0, 6/*S*/, "MPLMMMMM", "MMMLMM", "     START a PUBLIC LOTTERY     "),
            new Audience(GroupType.Landowners, 0, 0, "KMPMMMMM", "LMMMMM", "STOP MILITARY USE of THEIR LAND "),
            new Audience(GroupType.Landowners, 0, 0, "MIQMLMLM", "MKONMM", "  LOWER the BASIC MINIMUM WAGE  "),
            new Audience(GroupType.Landowners, 10/*W*/, -5/*H*/, "MMPMNMOI", "MMNMMM", "NATIONALISE AMERICAN BUSINESSES "),
            new Audience(GroupType.Landowners, 0, 5/*R*/, "MMPMJMLM", "MNOMLM", "LEVY DUTY on ALL LEFTOTO IMPORTS"),
            new Audience(GroupType.Landowners, 0, 4/*Q*/, "NNPMMIMM", "NMNNMK", " CUT SPENDING on the S. POLICE  "),
            new Audience(GroupType.Landowners, 0, -5/*H*/, "MMQMMMMM", "MMOMMM", "  DECREASE HEAVY LAND TAXATION  "),
            new Audience(GroupType.Landowners, 0, 0, "KLPMMMMM", "LLNNMM", "RELEASE TROOPS to WORK the LAND "),
            new Audience(GroupType.Landowners, -12/*A*/, -10/*C*/, "NNPMJMON", "MMPMKM", "BUILD a LARGE IRRIGATION SYSTEM ")
        ];
    }

    private void LoadDecisionsData()
    {
        Decisions =
        [
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 0, 0, "QLLMMLMM", "NMMLML", "MAKE ARMY CHIEF \"VICE-PRESIDENT\""),
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, -1/*L*/, -4/*I*/, "LQNMOMNM", "MMMLMM", "SET UP FREE CLINICS for WORKERS "),
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 0, 0, "LKQMMLLM", "LLOMML", "GIVE LANDOWNERS REGIONAL POWERS "),
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 5/*R*/, 0, "KMMMQMKN", "LMMLPM", "SELL AMERICAN ARMS to LEFTOTO   "),
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 12/*Y*/, 0, "MMLMLMKP", "MMMMMM", "SELL MINING RIGHTS to U.S. FIRMS"),
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 0, 10/*W*/, "KMMMMMPJ", "MMMMMM", "RENT the RUSSIANS a NAVAL BASE  "),

            new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None, 0, -8/*E*/, "NPPMMMMM", "LMMLMM", "DECREASE GENERAL TAXATION LEVEL "),
            new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None, -8/*E*/, 0, "PPPMMMMM", "MMMLMM", "STAGE a BIG POPULARITY CAMPAIGN "),
            new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None, 0, 8/*U*/, "PPPMMDMM", "ONNNMD", "CUT S.POLICE POWERS COMPLETELY  "),

            new Decision(DecisionType.ImproveYourChances, DecisionSubType.None, 0, -6/*G*/, "JJJMMUMM", "LLLLMU", "INCREASE S.POLICE POWERS a LOT  "),
            new Decision(DecisionType.ImproveYourChances, DecisionSubType.IncreaseBodyGuard, -4/*I*/, 0, "KLLMMLMM", "KMMMML", "INCREASE YOUR BODYGUARD        *"),
            new Decision(DecisionType.ImproveYourChances, DecisionSubType.PurchaseHelicopter, -12/*A*/, 0, "IIJMMKMM", "MMMMMM", "BUY an ESCAPE HELICOPTER        "),
            new Decision(DecisionType.ImproveYourChances, DecisionSubType.TransferToSwissAccount, 0, 0, "MMMMMMMM", "MMMMMM", "SEE TO YOUR SWISS BANK ACCOUNT *"),

            new Decision(DecisionType.RaiseSomeCash, DecisionSubType.AskForRussianLoan, 0, 0, "MMMMMMMM", "MMMMMM", "ASK the RUSSIANS for a \"LOAN\"  *"),
            new Decision(DecisionType.RaiseSomeCash, DecisionSubType.AskForAmericanLoan, 0, 0, "MMMMMMMM", "MMMMMM", "ASK AMERICANS for FOREIGN \"AID\"*"),
            new Decision(DecisionType.RaiseSomeCash, DecisionSubType.None, 12/*Z*/, 0, "NNPMGMKM", "MMMMMM", "NATIONALISE LEFTOTAN BUSINESSES "),

            new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None, -5/*H*/, 0, "PMMMJMLM", "RMMKKL", "BUY HEAVY ARTILLERY for THE ARMY"),
            new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None, 0, 0, "MPLMMLMM", "MRLPML", "ALLOW PEASANTS FREE MOVEMENT    "),
            new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None, 0, 0, "LLPMMLMM", "LLRLML", "ALLOW LANDOWNERS PRIVATE MILITIA")
        ];
    }

    private void LoadNewsData()
    {
        News =
        [
            new News(0, 0, "MMMMMIMM", "MMMQMI", " PRESIDENT LOSES S.POLICE FILES "),
            new News(0, 0, "MMMMMMMM", "LMMVMM", " CUBANS ARM and TRAIN GUERILLAS "),
            new News(0, 0, "MMMMMMMM", "IMMOMN", "ACCIDENT. ARMY BARRACKS BLOWS UP"),
            new News(0, 0, "MMMMMMMM", "MMJMKM", "   BANANA PRICES FALL by 98%    "),
            new News(0, 0, "MMMMMMMM", "MMOMIM", "  MAJOR EARTHQUAKE in LEFTOTO   "),
            new News(0, 0, "MMMMMMMM", "MILKMM", "A PLAGUE SWEEPS through PEASANTS")
        ];
    }
}
