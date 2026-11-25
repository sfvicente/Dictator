using Dictator.Core.Models;

namespace Dictator.Core.Services.Tests;

[TestFixture]
public class AssassinationServiceTests
{
    private Mock<IRandomService> _randomServiceMock;
    private Mock<IGroupService> _groupServiceMock;
    private Mock<IStatsService> _statsServiceMock;
    private AssassinationService _assassinationService;

    [SetUp]
    public void Setup()
    {
        _randomServiceMock = new Mock<IRandomService>();
        _groupServiceMock = new Mock<IGroupService>();
        _statsServiceMock = new Mock<IStatsService>();
        _assassinationService = new AssassinationService(
            _randomServiceMock.Object,
            _groupServiceMock.Object,
            _statsServiceMock.Object);
    }

    [Test]
    public void IsAssassinationSuccessful_WhenPlayerIsUnlucky_ReturnsTrue()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(0, 2))
            .Returns(0); // Player is unlucky
        _groupServiceMock
            .Setup(service => service.DoesMainPopulationHatePlayer())
            .Returns(false);
        _statsServiceMock
            .Setup(service => service.GetMonthlyMinimalPopularityAndStrength())
            .Returns(0); // Assuming minimum

        // Act
        bool isSuccessful = _assassinationService.IsAssassinationSuccessful();

        // Assert
        Assert.IsTrue(isSuccessful); // The assassination attempt should be successful
    }

    [Test]
    public void IsAssassinationSuccessful_WhenMainPopulationHatesPlayer_ReturnsTrue()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(0, 2))
            .Returns(1); // Player is lucky
        _groupServiceMock
            .Setup(service => service.DoesMainPopulationHatePlayer())
            .Returns(true);
        _statsServiceMock
            .Setup(service => service.GetMonthlyMinimalPopularityAndStrength())
            .Returns(0); // Assuming minimum

        // Act
        bool isSuccessful = _assassinationService.IsAssassinationSuccessful();

        // Assert
        Assert.IsTrue(isSuccessful); // The assassination attempt should be successful
    }

    [Test]
    public void IsAssassinationSuccessful_WhenPoliceHatesPlayer_ReturnsTrue()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(0, 2))
            .Returns(1); // Player is lucky
        _groupServiceMock
            .Setup(service => service.DoesMainPopulationHatePlayer())
            .Returns(false);
        _statsServiceMock
            .Setup(service => service.DoesPoliceHatePlayer())
            .Returns(true);
        _statsServiceMock
            .Setup(service => service.IsPoliceUnableToProtectPlayer())
            .Returns(false);

        // Act
        bool isSuccessful = _assassinationService.IsAssassinationSuccessful();

        // Assert
        Assert.IsTrue(isSuccessful); // The assassination attempt should be successful
    }

    [Test]
    public void IsAssassinationSuccessful_WhenPoliceIsUnableToProtectPlayer_ReturnsTrue()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(0, 2))
            .Returns(1); // Player is lucky
        _groupServiceMock
            .Setup(service => service.DoesMainPopulationHatePlayer())
            .Returns(false);
        _statsServiceMock
            .Setup(service => service.DoesPoliceHatePlayer())
            .Returns(false);
        _statsServiceMock
            .Setup(service => service.IsPoliceUnableToProtectPlayer())
            .Returns(true);

        // Act
        bool isSuccessful = _assassinationService.IsAssassinationSuccessful();

        // Assert
        Assert.IsTrue(isSuccessful); // The assassination attempt should be successful
    }

    [Test]
    public void GetAssassinationGroupName_ReturnsGroupName()
    {
        // Arrange
        GroupType groupType = GroupType.Peasants;
        string expectedGroupName = "Peasants";

        _groupServiceMock
            .Setup(service => service.GetGroupByType(groupType))
            .Returns(new Group(groupType, It.IsAny<int>(), It.IsAny<int>(), expectedGroupName, expectedGroupName));

        // Act
        string groupName = _assassinationService.GetAssassinationGroupName(groupType);

        // Assert
        Assert.AreEqual(expectedGroupName, groupName);
    }

    [Test]
    public void IsAssassinationSuccessful_WhenPlayerIsProtectedAndLucky_ReturnsFalse()
    {
        // Arrange
        _randomServiceMock.Setup(service => service.Next(0, 2)).Returns(1);
        _groupServiceMock.Setup(service => service.DoesMainPopulationHatePlayer()).Returns(false);
        _statsServiceMock.Setup(service => service.DoesPoliceHatePlayer()).Returns(false);
        _statsServiceMock.Setup(service => service.IsPoliceUnableToProtectPlayer()).Returns(false);

        // Act
        bool isSuccessful = _assassinationService.IsAssassinationSuccessful();

        // Assert
        Assert.IsFalse(isSuccessful);
    }

    [Test]
    public void ShouldAssassinationAttemptHappen_WhenSelectedGroupHasAssassinationStatus_ReturnsTrueAndGroupType()
    {
        // Arrange
        var army = new Group(GroupType.Army, 1, 6, "The ARMY", "   ARMY   ") { Status = GroupStatus.Default };
        var peasants = new Group(GroupType.Peasants, 1, 6, "The PEASANTS", " PEASANTS ") { Status = GroupStatus.Assassination };
        var landowners = new Group(GroupType.Landowners, 7, 6, "The LANDOWNERS", "LANDOWNERS") { Status = GroupStatus.Default };
        _groupServiceMock.Setup(g => g.GetGroups()).Returns([army, peasants, landowners]);
        _randomServiceMock.Setup(r => r.Next(3)).Returns(1);

        // Act
        var (shouldHappen, groupType) = _assassinationService.ShouldAssassinationAttemptHappen();

        // Assert
        Assert.IsTrue(shouldHappen);
        Assert.AreEqual(GroupType.Peasants, groupType);
    }

    [Test]
    public void ShouldAssassinationAttemptHappen_WhenSelectedGroupDoesNotPlotAssassination_ReturnsFalseAndNull()
    {
        // Arrange
        var army = new Group(GroupType.Army, 7, 6, "The ARMY", "   ARMY   ") { Status = GroupStatus.Default };
        _groupServiceMock.Setup(g => g.GetGroups()).Returns([army]);
        _randomServiceMock.Setup(r => r.Next(3)).Returns(0);

        // Act
        var (shouldHappen, groupType) = _assassinationService.ShouldAssassinationAttemptHappen();

        // Assert
        Assert.IsFalse(shouldHappen);
        Assert.IsNull(groupType);
    }
}