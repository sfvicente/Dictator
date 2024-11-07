using Dictator.Core.Services;
using Dictator.Core.Models;

namespace Dictator.Core.Tests.Services;

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
        var group1 = new Group(GroupType.Army, 30, 10, string.Empty, string.Empty);
        var group2 = new Group(GroupType.Peasants, 20, 10, string.Empty, string.Empty);
        var group3 = new Group(GroupType.Landowners, 10, 10, string.Empty, string.Empty);

        _groupServiceMock
            .Setup(g => g.GetGroups())
            .Returns([group1, group2, group3]);
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.SecretPolice))
            .Returns(new Group(GroupType.SecretPolice, 20, 20, string.Empty, string.Empty));
        _statsService.PlayerStrength = 5;
        _statsService.MonthlyMinimalPopularityAndStrength = 5;

        // Act
        int result = _statsService.CalculateRitimbaStrength();

        // Assert
        Assert.AreEqual(55, result);
    }

    [Test]
    public void CalculateLeftotoStrength_ShouldReturnCorrectValue()
    {
        // Arrange
        var group1 = new Group(GroupType.Army, 5, 10, string.Empty, string.Empty);
        var group2 = new Group(GroupType.Peasants, 5, 10, string.Empty, string.Empty);
        var group3 = new Group(GroupType.Landowners, 5, 10, string.Empty, string.Empty);
        var group4 = new Group(GroupType.Guerillas, 5, 10, string.Empty, string.Empty);
        var group5 = new Group(GroupType.Leftotans, 5, 10, string.Empty, string.Empty);
        var group6 = new Group(GroupType.SecretPolice, 5, 10, string.Empty, string.Empty);

        _groupServiceMock
            .Setup(g => g.GetGroups())
            .Returns([group1, group2, group3, group4, group5, group6]);
        _statsService.MonthlyMinimalPopularityAndStrength = 15;

        // Act
        int result = _statsService.CalculateLeftotoStrength();

        // Assert
        Assert.AreEqual(60, result);
    }
}
