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
        string groupName = _assassinationService.GetAssassinationGroupName();

        // Assert
        Assert.AreEqual(expectedGroupName, groupName);
    }
}