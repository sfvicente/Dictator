using System;
using Dictator.Core.Services;

namespace Dictator.Core.Tests.Services;

[TestFixture]
public class GovernmentServiceTests
{
    private Mock<IRandomService> _randomServiceMock;
    private Government _government;
    private GovernmentService _governmentService;

    [SetUp]
    public void SetUp()
    {
        _randomServiceMock = new Mock<IRandomService>();
        _government = new Government();
        _governmentService = new GovernmentService(_randomServiceMock.Object, _government);
    }

    [Test]
    public void Initialise_ResetsGovernmentStateWhilePreservingHighScore()
    {
        _governmentService.SetHighScore(120);
        _governmentService.AdvanceMonth();
        _governmentService.PurchaseHelicopter();
        _governmentService.KillPlayer();
        _governmentService.SetPlotBonus(5);

        _governmentService.Initialise();

        Assert.IsTrue(_governmentService.IsPlayerAlive());
        Assert.IsFalse(_governmentService.HasPlayerPurchasedHelicopter());
        Assert.AreEqual(0, _governmentService.GetMonth());
        Assert.AreEqual(0, _governmentService.GetPlotBonus());
        Assert.AreEqual(10, _governmentService.GetMonthlyRevolutionStrength());
        Assert.AreEqual(120, _governmentService.GetLastScore());
    }

    [Test]
    public void AdvanceMonth_IncrementsMonthByOne()
    {
        _governmentService.AdvanceMonth();
        _governmentService.AdvanceMonth();

        Assert.AreEqual(2, _governmentService.GetMonth());
    }

    [Test]
    public void PurchaseHelicopter_SetsHasHelicopterTrue()
    {
        _governmentService.PurchaseHelicopter();

        Assert.IsTrue(_governmentService.HasPlayerPurchasedHelicopter());
    }

    [Test]
    public void KillPlayer_SetsIsPlayerAliveFalse()
    {
        _governmentService.KillPlayer();

        Assert.IsFalse(_governmentService.IsPlayerAlive());
    }

    [Test]
    public void SetMonthlyRevolutionStrength_UsesRandomRange10To12()
    {
        _randomServiceMock.Setup(r => r.Next(10, 13)).Returns(12);

        _governmentService.SetMonthlyRevolutionStrength();

        Assert.AreEqual(12, _governmentService.GetMonthlyRevolutionStrength());
    }

    [Test]
    public void SetPlotBonus_WithNegativeValue_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _governmentService.SetPlotBonus(-1));
    }
}
