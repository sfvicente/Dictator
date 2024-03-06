using System;
using System.Linq;

namespace Dictator.Core.Services;

/// <summary>
///     Provides functionality related to the presidential decision mechanic.
/// </summary>
public interface IDecisionService
{
    public void Initialise();

    /// <summary>
    ///     Retrieves all the decisions of a specific type.
    /// </summary>
    /// <param name="decisionType">The type of decision.</param>
    /// <returns>An array of decisions of the specified type.</returns>
    public Decision[] GetDecisionsByType(DecisionType decisionType);

    /// <summary>
    ///     Retrieves a decisions with the specified type and index.
    /// </summary>
    /// <param name="decisionType">The type of decision.</param>
    /// <param name="optionSelected">The position of the decision within the group of decisions with the specified type.</param>
    /// <returns>The position that matches the specified type and at the specific position.</returns>
    public Decision GetDecisionByTypeAndIndex(DecisionType decisionType, int optionNumber);

    /// <summary>
    ///     Determines if a presidential option exists and is available for selection.
    /// </summary>
    /// <param name="decisionType">The type of decision.</param>
    /// <param name="optionNumber">The option number within the type group of the decision.</param>
    /// <returns><c>true</c> if the presidential decision exists and is available for selection; otherwise, <c>false</c>.</returns>
    public bool DoesPresidentialOptionExistAndIsAvailable(DecisionType decisionType, int optionNumber);

    /// <summary>
    ///     Marks a presidential decision option as used.
    /// </summary>
    /// <param name="text">The text of the presidential decision which will be marked as used.</param>
    public void MarkDecisionAsUsed(string text);

    /// <summary>
    ///     Applies the effects of a decision on the groups popularity and strength with the costs of treasury.
    /// </summary>
    /// <param name="decision">The decision whose effects will be apply.</param>
    void ApplyDecisionEffects(Decision decision);
}

/// <summary>
///     Provides functionality related to the presidential decision mechanic.
/// </summary>
public class DecisionService : IDecisionService
{
    private Decision[] decisions;
    private readonly IAccountService accountService;
    private readonly IGroupService groupService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DecisionService"/> class from a <see cref="IAccountService"/> and
    ///     a <see cref="IGroupService"/> components.
    /// </summary>
    /// <param name="accountService">The service used to provide functionality related to the treasury and associated
    /// costs and the Swiss bank account.</param>
    /// <param name="groupService">The service used to provide functionality related to the groups or factions.</param>
    public DecisionService(IAccountService accountService, IGroupService groupService)
    {
        this.accountService = accountService;
        this.groupService = groupService;
        Initialise();
    }

    public void Initialise()
    {
        decisions = new Decision[] {
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 0, 0, "QLLMMLMM", "NMMLML", "MAKE ARMY CHIEF \"VICE-PRESIDENT\""),
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, -1/*L*/, -4/*I*/, "LQNMOMNM", "MMMLMM", "SET UP FREE CLINICS for WORKERS "),
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 0, 0, "LKQMMLLM", "LLOMML", "GIVE LANDOWNERS REGIONAL POWERS "),
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 5/*R*/, 0, "KMMMQMKN", "LMMLPM", "SELL AMERICAN ARMS to LEFTOTO   "),
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None,12/*Y*/, 0, "MMLMLMKP", "MMMMMM", "SELL MINING RIGHTS to U.S. FIRMS"),
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None,0, 10/*W*/, "KMMMMMPJ", "MMMMMM", "RENT the RUSSIANS a NAVAL BASE  "),

            new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None,0, -8/*E*/, "NPPMMMMM", "LMMLMM", "DECREASE GENERAL TAXATION LEVEL "),
            new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None, -8/*E*/, 0, "PPPMMMMM", "MMMLMM", "STAGE a BIG POPULARITY CAMPAIGN "),
            new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None,0, 8/*U*/, "PPPMMDMM", "ONNNMD", "CUT S.POLICE POWERS COMPLETELY  "),

            new Decision(DecisionType.ImproveYourChanges, DecisionSubType.None, 0, -6/*G*/, "JJJMMUMM", "LLLLMU", "INCREASE S.POLICE POWERS a LOT  "),
            new Decision(DecisionType.ImproveYourChanges, DecisionSubType.IncreaseBodyGuard, -4/*I*/, 0, "KLLMMLMM", "KMMMML", "INCREASE YOUR BODYGUARD        *"),
            new Decision(DecisionType.ImproveYourChanges, DecisionSubType.PurchaseHelicopter, -12/*A*/, 0, "IIJMMKMM", "MMMMMM", "BUY an ESCAPE HELICOPTER        "),
            new Decision(DecisionType.ImproveYourChanges, DecisionSubType.TransferToSwissAccount, 0, 0, "MMMMMMMM", "MMMMMM", "SEE TO YOUR SWISS BANK ACCOUNT *"),

            new Decision(DecisionType.RaiseSomeCash, DecisionSubType.AskForRussianLoan, 0, 0, "MMMMMMMM", "MMMMMM", "ASK the RUSSIANS for a \"LOAN\"  *"),
            new Decision(DecisionType.RaiseSomeCash, DecisionSubType.AskForAmericanLoan, 0, 0, "MMMMMMMM", "MMMMMM", "ASK AMERICANS for FOREIGN \"AID\"*"),
            new Decision(DecisionType.RaiseSomeCash, DecisionSubType.None, 12/*Z*/, 0, "NNPMGMKM", "MMMMMM", "NATIONALISE LEFTOTAN BUSINESSES "),

            new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None, -5/*H*/, 0, "PMMMJMLM", "RMMKKL", "BUY HEAVY ARTILLERY for THE ARMY"),
            new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None,0, 0, "MPLMMLMM", "MRLPML", "ALLOW PEASANTS FREE MOVEMENT    "),
            new Decision(DecisionType.StrengthenAGroup, DecisionSubType.None,0, 0, "LLPMMLMM", "LLRLML", "ALLOW LANDOWNERS PRIVATE MILITIA")
        };
    }

    /// <summary>
    ///     Retrieves all the decisions of a specific type.
    /// </summary>
    /// <param name="decisionType">The type of decision.</param>
    /// <returns>An array of decisions of the specified type.</returns>
    public Decision[] GetDecisionsByType(DecisionType decisionType)
    {
        Decision[] decisionCopy = (Decision[])decisions.Clone();

        return decisionCopy.Where(x => x.Type == decisionType).ToArray();
    }

    /// <summary>
    ///     Retrieves a decisions with the specified type and index.
    /// </summary>
    /// <param name="decisionType">The type of decision.</param>
    /// <param name="optionSelected">The position of the decision within the group of decisions with the specified type.</param>
    /// <returns>The position that matches the specified type and at the specific position.</returns>
    public Decision GetDecisionByTypeAndIndex(DecisionType decisionType, int optionNumber)
    {
        if (optionNumber < 0)
        {
            throw new ArgumentException(nameof(optionNumber));
        }

        Decision[] decisions = GetDecisionsByType(decisionType);

        if (optionNumber > decisions.Length)
        {
            throw new ArgumentException(nameof(optionNumber));
        }

        return decisions[optionNumber - 1];
    }

    /// <summary>
    ///     Determines if a presidential option exists and is available for selection.
    /// </summary>
    /// <param name="decisionType">The type of decision.</param>
    /// <param name="optionNumber">The option number within the type group of the decision.</param>
    /// <returns><c>true</c> if the presidential decision exists and is available for selection; otherwise, <c>false</c>.</returns>
    public bool DoesPresidentialOptionExistAndIsAvailable(DecisionType decisionType, int optionNumber)
    {
        Decision[] decisions = GetDecisionsByType(decisionType);

        if (optionNumber > decisions.Length)
        {
            return false;
        }

        if (decisions[optionNumber - 1].HasBeenUsed)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Marks a presidential decision option as used.
    /// </summary>
    /// <param name="text">The text of the presidential decision which will be marked as used.</param>
    public void MarkDecisionAsUsed(string text)
    {
        Decision item = decisions.Where(x => x.Text == text).Single();

        item.HasBeenUsed = true;
    }

    /// <summary>
    ///     Applies the effects of a decision on the groups popularity and strength with the costs of treasury.
    /// </summary>
    /// <param name="decision">The decision whose effects will be apply.</param>
    public void ApplyDecisionEffects(Decision decision)
    {
        ArgumentNullException.ThrowIfNull(decision);

        groupService.ApplyPopularityChange(decision.GroupPopularityChanges);
        groupService.ApplyStrengthChange(decision.GroupStrengthChanges);
        accountService.ApplyTreasuryChanges(decision.Cost, decision.MonthlyCost);
    }
}
