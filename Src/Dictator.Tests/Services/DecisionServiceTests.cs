using System;
using System.ComponentModel;
using Dictator.Core.Models;
using Dictator.Core.Services;

namespace Dictator.Core.Tests.Services;

[TestFixture]
public class DecisionServiceTests
{
    private Mock<IGameState> _gameStateMock;
    private Mock<IAccountService> _accountServiceMock;
    private Mock<IGroupService> _groupServiceMock;
    private DecisionService _decisionService;
    private Decision[] _sampleDecisions;

    [SetUp]
    public void SetUp()
    {
        _gameStateMock = new Mock<IGameState>();
        _accountServiceMock = new Mock<IAccountService>();
        _groupServiceMock = new Mock<IGroupService>();
        _decisionService = new DecisionService(_gameStateMock.Object, _accountServiceMock.Object, _groupServiceMock.Object);

        _sampleDecisions =
        [
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, 0, 0, "QLLMMLMM", "NMMLML", "MAKE ARMY CHIEF \"VICE-PRESIDENT\""),
            new Decision(DecisionType.PleaseAGroup, DecisionSubType.None, -1, -4, "LQNMOMNM", "MMMLMM", "SET UP FREE CLINICS for WORKERS "),
            new Decision(DecisionType.PleaseAllGroups, DecisionSubType.None, 0, -8, "NPPMMMMM", "LMMLMM", "DECREASE GENERAL TAXATION LEVEL ")
        ];

        _gameStateMock.Setup(g => g.GetDecisions()).Returns(_sampleDecisions);
    }

    [Test]
    public void GetDecisionsByType_ReturnsMatchingDecisions()
    {
        Decision[] result = _decisionService.GetDecisionsByType(DecisionType.PleaseAGroup);

        Assert.AreEqual(2, result.Length);
        Assert.AreEqual("MAKE ARMY CHIEF \"VICE-PRESIDENT\"", result[0].Text);
        Assert.AreEqual("SET UP FREE CLINICS for WORKERS ", result[1].Text);
    }

    [Test]
    public void GetDecisionByTypeAndIndex_WithValidOption_ReturnsExpectedDecision()
    {
        Decision result = _decisionService.GetDecisionByTypeAndIndex(DecisionType.PleaseAGroup, 2);

        Assert.AreEqual("SET UP FREE CLINICS for WORKERS ", result.Text);
    }

    [Test]
    public void GetDecisionByTypeAndIndex_WhenOptionExceedsCount_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _decisionService.GetDecisionByTypeAndIndex(DecisionType.PleaseAGroup, 5));
    }

    [Test]
    public void DoesPresidentialOptionExistAndIsAvailable_WhenUnused_ReturnsTrue()
    {
        bool available = _decisionService.DoesPresidentialOptionExistAndIsAvailable(DecisionType.PleaseAGroup, 1);

        Assert.IsTrue(available);
    }

    [Test]
    public void DoesPresidentialOptionExistAndIsAvailable_WhenAlreadyUsed_ReturnsFalse()
    {
        _sampleDecisions[0].HasBeenUsed = true;

        bool available = _decisionService.DoesPresidentialOptionExistAndIsAvailable(DecisionType.PleaseAGroup, 1);

        Assert.IsFalse(available);
    }

    [Test]
    public void DoesPresidentialOptionExistAndIsAvailable_WhenOptionOutOfBounds_ReturnsFalse()
    {
        bool available = _decisionService.DoesPresidentialOptionExistAndIsAvailable(DecisionType.PleaseAGroup, 99);

        Assert.IsFalse(available);
    }

    [Test]
    public void MarkDecisionAsUsed_SetsHasBeenUsedTrue()
    {
        _decisionService.MarkDecisionAsUsed("DECREASE GENERAL TAXATION LEVEL ");

        Assert.IsTrue(_sampleDecisions[2].HasBeenUsed);
    }

    [Test]
    public void ApplyDecisionEffects_InvokesGroupAndAccountServices()
    {
        Decision decision = _sampleDecisions[1];

        _decisionService.ApplyDecisionEffects(decision);

        _groupServiceMock.Verify(g => g.ApplyPopularityChange(decision.GroupPopularityChanges), Times.Once);
        _groupServiceMock.Verify(g => g.ApplyStrengthChange(decision.GroupStrengthChanges), Times.Once);
        _accountServiceMock.Verify(a => a.ApplyTreasuryChanges(decision.Cost, decision.MonthlyCost), Times.Once);
    }
}
