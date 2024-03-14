using System;
using System.Linq;
using Dictator.Core.Models;

namespace Dictator.Core.Services;

public interface IDecisionService
{
    public Decision[] GetDecisionsByType(Decision[] decisions, DecisionType decisionType);
    public Decision GetDecisionByTypeAndIndex(Decision[] decisions, DecisionType decisionType, int optionNumber);
    public bool DoesPresidentialOptionExistAndIsAvailable(Decision[] decisions,DecisionType decisionType, int optionNumber);
    public void MarkDecisionAsUsed(Decision[] decisions, string text);
    void ApplyDecisionEffects(Decision decision);
}

public class DecisionService : IDecisionService
{
    private readonly IAccountService _accountService;
    private readonly IGroupService _groupService;

    public DecisionService(IAccountService accountService, IGroupService groupService)
    {
        _accountService = accountService;
        _groupService = groupService;
    }

    /// <summary>
    ///     Retrieves all the decisions of a specific type.
    /// </summary>
    /// <param name="decisionType">The type of decision.</param>
    /// <returns>An array of decisions of the specified type.</returns>
    public Decision[] GetDecisionsByType(Decision[] decisions, DecisionType decisionType)
    {
        return decisions.Where(x => x.Type == decisionType).ToArray();
    }

    /// <summary>
    ///     Retrieves a decisions with the specified type and index.
    /// </summary>
    /// <param name="decisionType">The type of decision.</param>
    /// <param name="optionSelected">The position of the decision within the group of decisions with the specified type.</param>
    /// <returns>The position that matches the specified type and at the specific position.</returns>
    public Decision GetDecisionByTypeAndIndex(Decision[] decisions, DecisionType decisionType, int optionNumber)
    {
        ArgumentNullException.ThrowIfNull(decisions);
        ArgumentOutOfRangeException.ThrowIfNegative(optionNumber);

        Decision[] filteredDecisions = GetDecisionsByType(decisions, decisionType);

        ArgumentOutOfRangeException.ThrowIfGreaterThan(optionNumber, filteredDecisions.Length);

        return filteredDecisions[optionNumber - 1];
    }

    /// <summary>
    ///     Determines if a presidential option exists and is available for selection.
    /// </summary>
    /// <param name="decisionType">The type of decision.</param>
    /// <param name="optionNumber">The option number within the type group of the decision.</param>
    /// <returns><c>true</c> if the presidential decision exists and is available for selection; otherwise, <c>false</c>.</returns>
    public bool DoesPresidentialOptionExistAndIsAvailable(Decision[] decisions, DecisionType decisionType, int optionNumber)
    {
        ArgumentNullException.ThrowIfNull(decisions);
        ArgumentOutOfRangeException.ThrowIfNegative(optionNumber);

        Decision[] filteredDecisions = GetDecisionsByType(decisions, decisionType);

        if (optionNumber > filteredDecisions.Length)
        {
            return false;
        }

        if (filteredDecisions[optionNumber - 1].HasBeenUsed)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Marks a presidential decision option as used.
    /// </summary>
    /// <param name="text">The text of the presidential decision which will be marked as used.</param>
    public void MarkDecisionAsUsed(Decision[] decisions, string text)
    {
        ArgumentNullException.ThrowIfNull(decisions);

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

        _groupService.ApplyPopularityChange(decision.GroupPopularityChanges);
        _groupService.ApplyStrengthChange(decision.GroupStrengthChanges);
        _accountService.ApplyTreasuryChanges(decision.Cost, decision.MonthlyCost);
    }
}
