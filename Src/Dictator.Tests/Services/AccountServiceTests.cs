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
}
