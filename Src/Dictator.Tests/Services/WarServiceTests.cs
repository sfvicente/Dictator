using Dictator.Core.Services;
using Dictator.Core.Models;

namespace Dictator.Tests;

[TestFixture]
public class WarServiceTests
{
    private Mock<IRandomService> _randomMock;
    private Mock<IGroupService> _groupServiceMock;
    private Mock<IStatsService> _statsServiceMock;
    private Mock<IGovernmentService> _governmentServiceMock;
    private WarService _warService;

    [SetUp]
    public void Setup()
    {
        _randomMock = new Mock<IRandomService>();
        _groupServiceMock = new Mock<IGroupService>();
        _statsServiceMock = new Mock<IStatsService>();
        _governmentServiceMock = new Mock<IGovernmentService>();

        _warService = new WarService(
            _randomMock.Object,
            _groupServiceMock.Object,
            _statsServiceMock.Object,
            _governmentServiceMock.Object);
    }

    [Test]
    public void ShouldWarHappen_WhenRandomNumberIsZero_ReturnsTrue()
    {
        // Arrange
        _randomMock
            .Setup(r => r.Next(3))
            .Returns(0);

        // Act
        bool result = _warService.ShouldWarHappen();

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void ShouldWarHappen_WhenRandomNumberIsNotZero_ReturnsFalse()
    {
        // Arrange
        _randomMock
            .Setup(r => r.Next(3))
            .Returns(1);

        // Act
        bool result = _warService.ShouldWarHappen();

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void ApplyThreatOfWarEffects_ShouldIncreasePopularityAndDecreaseSecretPolicePopularity()
    {
        // Act
        _warService.ApplyThreatOfWarEffects();

        // Assert
        _groupServiceMock.Verify(g => g.IncreasePopularity(GroupType.Army, 1), Times.Once);
        _groupServiceMock.Verify(g => g.IncreasePopularity(GroupType.Peasants, 1), Times.Once);
        _groupServiceMock.Verify(g => g.IncreasePopularity(GroupType.Landowners, 1), Times.Once);
        _groupServiceMock.Verify(g => g.DecreasePopularity(GroupType.SecretPolice, 1), Times.Once);
    }

    [Test]
    public void ExecuteWar_WhenRitimbanStrengthIsGreaterThanLeftotanStrength_ReturnsTrue()
    {
        // Arrange
        var warStats = new WarStats { RitimbanStrength = 100, LeftotanStrength = 50 };
        _randomMock
            .Setup(r => r.Next(3))
            .Returns(0); // Simulate Ritimba wins

        // Act
        bool result = _warService.ExecuteWar(warStats);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void DoesConflictExist_WhenLeftotansAreWeak_ReturnsFalse()
    {
        // Arrange
        var leftotoGroup = new Group(GroupType.Leftotans, 1, 1, string.Empty, string.Empty);
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.Leftotans))
            .Returns(leftotoGroup);

        // Act
        bool result = _warService.DoesConflictExist();

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void DoesConflictExist_WhenPlayerIsPopularWithLeftotans_ReturnsFalse()
    {
        // Arrange
        var leftotoGroup = new Group(GroupType.Leftotans, 100, 100, string.Empty, string.Empty);
        _groupServiceMock
            .Setup(g => g.GetGroupByType(GroupType.Leftotans))
            .Returns(leftotoGroup);

        // Act
        bool result = _warService.DoesConflictExist();

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void BeginInvasion_ReturnsWarStatsWithCalculatedStrengths()
    {
        // Arrange
        int ritimbaStrength = 10;
        _statsServiceMock
            .Setup(s => s.CalculateRitimbaStrength())
            .Returns(ritimbaStrength);
        int leftotoStrength = 5;
        _statsServiceMock
            .Setup(s => s.CalculateLeftotoStrength())
            .Returns(leftotoStrength);

        // Act
        var warStats = _warService.BeginInvasion();

        // Assert
        Assert.AreEqual(ritimbaStrength, warStats.RitimbanStrength);
        Assert.AreEqual(leftotoStrength, warStats.LeftotanStrength);
    }
}