using Dictator.Core.Services;
using Dictator.Core.Models;

namespace Dictator.Tests;

[TestFixture]
public class StatsServiceTests
{
    private Mock<IRandomService> _randomServiceMock;
    private Mock<IGroupService> _groupServiceMock;
    private StatsService _statsService;

    [SetUp]
    public void SetUp()
    {
        _randomServiceMock = new Mock<IRandomService>();
        _groupServiceMock = new Mock<IGroupService>();
        _statsService = new StatsService(_randomServiceMock.Object, _groupServiceMock.Object);
    }

    [Test]
    public void SetMonthlyMinimalPopularityAndStrength_ShouldSetValueWithinRange()
    {
        // Arrange
        _randomServiceMock
            .Setup(r => r.Next(2, 5))
            .Returns(3);

        // Act
        _statsService.SetMonthlyMinimalPopularityAndStrength();

        // Assert
        Assert.That(_statsService.MonthlyMinimalPopularityAndStrength, Is.InRange(2, 4));
    }

    [Test]
    public void DoesPoliceHatePlayer_WhenPolicePopularityBelowThreshold_ReturnsTrue()
    {
        // Arrange
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.SecretPolice))
            .Returns(new Group(GroupType.SecretPolice, 10, 10, string.Empty, string.Empty));
        _statsService.MonthlyMinimalPopularityAndStrength = 15;

        // Act
        bool result = _statsService.DoesPoliceHatePlayer();

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void DoesPoliceHatePlayer_WhenPolicePopularityAboveThreshold_ReturnsFalse()
    {
        // Arrange
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.SecretPolice))
            .Returns(new Group(GroupType.SecretPolice, 20, 10, string.Empty, string.Empty));
        _statsService.MonthlyMinimalPopularityAndStrength = 15;

        // Act
        bool result = _statsService.DoesPoliceHatePlayer();

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void IsPoliceUnableToProtectPlayer_WhenPoliceStrengthBelowThreshold_ReturnsTrue()
    {
        // Arrange
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.SecretPolice))
            .Returns(new Group(GroupType.SecretPolice, 20, 10, string.Empty, string.Empty));
        _statsService.MonthlyMinimalPopularityAndStrength = 15;

        // Act
        bool result = _statsService.IsPoliceUnableToProtectPlayer();

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void IsPoliceUnableToProtectPlayer_WhenPoliceStrengthAboveThreshold_ReturnsFalse()
    {
        // Arrange
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.SecretPolice))
            .Returns(new Group(GroupType.SecretPolice, 20, 20, string.Empty, string.Empty));
        _statsService.MonthlyMinimalPopularityAndStrength = 15;

        // Act
        bool result = _statsService.IsPoliceUnableToProtectPlayer();

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void GetPlayerStrength_ShouldReturnCorrectValue()
    {
        // Arrange
        _statsService.PlayerStrength = 5;

        // Act
        int result = _statsService.GetPlayerStrength();

        // Assert
        Assert.AreEqual(5, result);
    }

    [Test]
    public void IncreaseBodyguard_ShouldIncreasePlayerStrengthBy2()
    {
        // Arrange
        _statsService.PlayerStrength = 5;

        // Act
        _statsService.IncreaseBodyguard();

        // Assert
        Assert.AreEqual(7, _statsService.PlayerStrength);
    }

    [Test]
    public void DecreasePlayerStrength_WhenPlayerStrengthGreaterThanZero_ShouldDecreasePlayerStrengthBy1()
    {
        // Arrange
        _statsService.PlayerStrength = 5;

        // Act
        _statsService.DecreasePlayerStrength();

        // Assert
        Assert.AreEqual(4, _statsService.PlayerStrength);
    }

    [Test]
    public void DecreasePlayerStrength_WhenPlayerStrengthIsZero_ShouldNotDecreasePlayerStrength()
    {
        // Arrange
        _statsService.PlayerStrength = 0;

        // Act
        _statsService.DecreasePlayerStrength();

        // Assert
        Assert.AreEqual(0, _statsService.PlayerStrength);
    }

    [Test]
    public void CalculateRitimbaStrength_ShouldReturnCorrectValue()
    {
        // Arrange
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.Army))
            .Returns(new Group(GroupType.Army, 20, 10, string.Empty, string.Empty));
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.Peasants))
            .Returns(new Group(GroupType.Peasants, 20, 10, string.Empty, string.Empty));
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.Landowners))
            .Returns(new Group(GroupType.Landowners, 20, 10, string.Empty, string.Empty));
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.SecretPolice))
            .Returns(new Group(GroupType.SecretPolice, 20, 20, string.Empty, string.Empty));
        _statsService.PlayerStrength = 5;
        _statsService.MonthlyMinimalPopularityAndStrength = 15;

        // Act
        int result = _statsService.CalculateRitimbaStrength();

        // Assert
        Assert.AreEqual(65, result);
    }

    [Test]
    public void CalculateLeftotoStrength_ShouldReturnCorrectValue()
    {
        // Arrange
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.Americans))
            .Returns(new Group(GroupType.Americans, 10, 10, string.Empty, string.Empty));
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.Russians))
            .Returns(new Group(GroupType.Russians, 10, 10, string.Empty, string.Empty));
        _statsService.MonthlyMinimalPopularityAndStrength = 15;

        // Act
        int result = _statsService.CalculateLeftotoStrength();

        // Assert
        Assert.AreEqual(20, result);
    }
}
