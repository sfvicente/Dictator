using Dictator.Core.Services;
using Dictator.Core.Models;
using System.Linq;

namespace Dictator.Core.Tests.Services;

[TestFixture]
public class AudienceServiceTests
{
    private Mock<IRandomService> _randomServiceMock;
    private Mock<IAccountService> _accountServiceMock;
    private Mock<IGroupService> _groupServiceMock;
    private AudienceService _audienceService;

    [SetUp]
    public void SetUp()
    {
        _randomServiceMock = new Mock<IRandomService>();
        _accountServiceMock = new Mock<IAccountService>();
        _groupServiceMock = new Mock<IGroupService>();

        _audienceService = new AudienceService(_randomServiceMock.Object, _accountServiceMock.Object, _groupServiceMock.Object);
    }

    [Test]
    public void SelectRandomUnusedAudienceRequest_ShouldReturnRandomUnusedAudience()
    {
        // Arrange
        var audience1 = new Audience(GroupType.Army, 10, 20, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        var audience2 = new Audience(GroupType.Peasants, 20, 30, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        var audience3 = new Audience(GroupType.Landowners, 30, 40, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        var audiences = new Audience[] { audience1, audience2, audience3 };
        _randomServiceMock
            .Setup(r => r.Next(It.IsAny<int>()))
            .Returns(1);

        // Act
        var result = _audienceService.SelectRandomUnusedAudienceRequest(audiences);

        // Assert
        Assert.AreEqual(audience2, result);
        Assert.IsTrue(result.HasBeenUsed);
    }

    [Test]
    public void AcceptAudienceRequest_ShouldApplyChangesToPopularityStrengthAndTreasury()
    {
        // Arrange
        var audience = new Audience(GroupType.Army, 10, 20, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

        // Act
        _audienceService.AcceptAudienceRequest(audience);

        // Assert
        _groupServiceMock.Verify(g => g.ApplyPopularityChange(audience.GroupPopularityChanges), Times.Once);
        _groupServiceMock.Verify(g => g.ApplyStrengthChange(audience.GroupStrengthChanges), Times.Once);
        _accountServiceMock.Verify(a => a.ApplyTreasuryChanges(audience.Cost, audience.MonthlyCost), Times.Once);
    }

    [Test]
    public void RefuseAudienceRequest_ShouldDecreasePopularityWithRequester()
    {
        // Arrange
        var audience = new Audience(GroupType.Army, -8, 0, "PLMMIMLM", "NMNKMM", "ATTACK GUERILLA BASES in LEFTOTO");

        // Act
        _audienceService.RefuseAudienceRequest(audience);

        // Assert
        _groupServiceMock.Verify(g => g.DecreasePopularity(audience.Requester, It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void GetUnusedAudiences_WhenAudiencesAreUnused_ShouldReturnAllUnusedAudiences()
    {
        // Arrange
        var audience1 = new Audience(GroupType.Army, 10, 20, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        var audience2 = new Audience(GroupType.Peasants, 20, 30, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        var audience3 = new Audience(GroupType.Landowners, 30, 40, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        var audiences = new Audience[] { audience1, audience2, audience3 };

        // Act
        var result = _audienceService.GetUnusedAudiences(audiences);

        // Assert
        Assert.AreEqual(3, result.Count);
        Assert.IsTrue(result.Contains(audience1));
        Assert.IsTrue(result.Contains(audience2));
        Assert.IsTrue(result.Contains(audience3));
    }

    [Test]
    public void GetUnusedAudiences_WhenNoUnusedAudiencesExist_ShouldResetAllAudiencesAndReturnAllAudiences()
    {
        // Arrange
        var audience1 = new Audience(GroupType.Army, 10, 20, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        var audience2 = new Audience(GroupType.Peasants, 20, 30, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        var audience3 = new Audience(GroupType.Landowners, 30, 40, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

        audience1.HasBeenUsed = true;
        audience2.HasBeenUsed = true;
        audience3.HasBeenUsed = true;

        var audiences = new Audience[] { audience1, audience2, audience3 };

        // Act
        var result = _audienceService.GetUnusedAudiences(audiences);

        // Assert
        Assert.AreEqual(3, result.Count);
        Assert.IsFalse(result.Any(a => a.HasBeenUsed));
    }
}
