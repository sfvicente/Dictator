using Dictator.Core.Configuration;
using Dictator.Core.Models;
using Dictator.Core.Services;

namespace Dictator.Core.Tests.Services;

[TestFixture]
public class AccountServiceTests
{
    private GameState _gameState;
    private Mock<IGroupService> _groupServiceMock;
    private Mock<IStatsService> _statsServiceMock;
    private AccountService _accountService;

    [SetUp]
    public void Setup()
    {
        _gameState = new GameState(new AccountSettings());
        _groupServiceMock = new Mock<IGroupService>();
        _statsServiceMock = new Mock<IStatsService>();
        _accountService = new AccountService(_gameState, _groupServiceMock.Object, _statsServiceMock.Object);
    }

    [Test]
    public void TransferToSwissBankAccount_StealsHalfOfTreasuryAndOpensAccount()
    {
        // Act
        SwissBankAccountTransfer transfer = _accountService.TransferToSwissBankAccount();

        // Assert
        Assert.AreEqual(1000, transfer.TreasuryPreviousBalance);
        Assert.AreEqual(500, transfer.AmountStolen);
        Assert.AreEqual(500, _accountService.GetTreasuryBalance());
        Assert.AreEqual(500, _accountService.GetSwissBankAccountBalance());
        Assert.IsTrue(_accountService.GetAccount().HasSwissBankAccount);
    }

    [Test]
    public void ApplyBankruptcyEffects_DecreasesArmyAndPolicePopularityAndStrength()
    {
        // Act
        _accountService.ApplyBankruptcyEffects();

        // Assert
        _groupServiceMock.Verify(g => g.DecreasePopularity(GroupType.Army, 1), Times.Once);
        _groupServiceMock.Verify(g => g.DecreasePopularity(GroupType.SecretPolice, 1), Times.Once);
        _groupServiceMock.Verify(g => g.DecreaseStrength(GroupType.SecretPolice), Times.Once);
        _statsServiceMock.Verify(s => s.DecreasePlayerStrength(), Times.Once);
    }

    [Test]
    public void PayMonthlyCosts_WhenTreasuryPositive_DeductsMonthlyCosts()
    {
        _accountService.PayMonthlyCosts();

        Assert.AreEqual(940, _accountService.GetTreasuryBalance());
    }

    [Test]
    public void PayMonthlyCosts_WhenTreasuryZeroOrNegative_DoesNotDeductMonthlyCosts()
    {
        _accountService.PayFromTreasury(1000);

        _accountService.PayMonthlyCosts();

        Assert.AreEqual(0, _accountService.GetTreasuryBalance());
        Assert.IsTrue(_accountService.IsGovernmentBankrupt());
    }

    [Test]
    public void ApplyTreasuryChanges_UpdatesBalanceAndClampsMonthlyCostsAtZero()
    {
        // cost = -10 -> -100 treasury; monthlyCost = 80 -> 60 - 80 clamped to 0
        _accountService.ApplyTreasuryChanges(-10, 80);

        Assert.AreEqual(900, _accountService.GetTreasuryBalance());
        Assert.AreEqual(0, _accountService.GetMonthlyCosts());
    }

    [Test]
    public void TransferToSwissBankAccount_WhenTreasuryZero_TransfersNothingAndDoesNotOpenAccount()
    {
        _accountService.PayFromTreasury(1000);

        SwissBankAccountTransfer transfer = _accountService.TransferToSwissBankAccount();

        Assert.AreEqual(0, transfer.AmountStolen);
        Assert.IsFalse(_accountService.GetAccount().HasSwissBankAccount);
    }

    [Test]
    public void CanAfford_WhenTreasuryCoversCost_ReturnsTrue()
    {
        var decision = new Decision(DecisionType.ImproveYourChances, DecisionSubType.PurchaseHelicopter, -12, 0, "IIJMMKMM", "MMMMMM", "BUY an ESCAPE HELICOPTER");

        Assert.IsTrue(_accountService.CanAfford(decision));
    }

    [Test]
    public void CanAfford_WhenTreasuryCannotCoverCost_ReturnsFalse()
    {
        _accountService.PayFromTreasury(950); // Balance is now 50, helicopter costs -12 * 10 = -120
        var decision = new Decision(DecisionType.ImproveYourChances, DecisionSubType.PurchaseHelicopter, -12, 0, "IIJMMKMM", "MMMMMM", "BUY an ESCAPE HELICOPTER");

        Assert.IsFalse(_accountService.CanAfford(decision));
    }

    [Test]
    public void CanAfford_WhenTreasuryZeroAndMonthlyCostIncreases_ReturnsFalse()
    {
        _accountService.PayFromTreasury(1000); // Balance is 0, monthlyCost = -5
        var audience = new Audience(GroupType.Army, 0, -5, "QJLMMMMM", "PKLMMM", "INTRODUCE CONSCRIPTION");

        Assert.IsFalse(_accountService.CanAfford(audience));
    }
}
