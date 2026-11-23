using Dictator.Core.Models;

namespace Dictator.Core.Services.Tests;

[TestFixture]
public class EscapeServiceTests
{
    private Mock<IRandomService> _randomServiceMock;
    private Mock<IGroupService> _groupServiceMock;
    private EscapeService _escapeService;

    [SetUp]
    public void Setup()
    {
        _randomServiceMock = new Mock<IRandomService>();
        _groupServiceMock = new Mock<IGroupService>();
        _escapeService = new EscapeService(_randomServiceMock.Object, _groupServiceMock.Object);
    }

    [Test]
    public void IsPlayerAbleToEscapeAfterLosingWar_WhenRandomIsLessThanTwo_ReturnsTrue()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(3))
            .Returns(0);

        // Act
        bool hasEscaped = _escapeService.IsPlayerAbleToEscapeAfterLosingWar();

        // Assert
        Assert.IsTrue(hasEscaped);
    }

    [Test]
    public void IsPlayerAbleToEscapeAfterLosingWar_WhenRandomIsTwo_ReturnsFalse()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(3))
            .Returns(2);

        // Act
        bool hasEscaped = _escapeService.IsPlayerAbleToEscapeAfterLosingWar();

        // Assert
        Assert.IsFalse(hasEscaped);
    }

    [Test]
    public void IsPlayerAbleToEscapeByHelicopter_WhenRandomIsNonZero_ReturnsTrue()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(4))
            .Returns(1);

        // Act
        bool isEscaped = _escapeService.IsPlayerAbleToEscapeByHelicopter();

        // Assert
        Assert.IsTrue(isEscaped);
    }

    [Test]
    public void IsPlayerAbleToEscapeByHelicopter_WhenRandomIsZero_ReturnsFalse()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(4))
            .Returns(0);

        // Act
        bool isEscaped = _escapeService.IsPlayerAbleToEscapeByHelicopter();

        // Assert
        Assert.IsFalse(isEscaped);
    }

    [Test]
    public void DoesGuerrillaCatchPlayerEscaping_WhenRandomIsZero_ReturnsTrue()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(0, 4))
            .Returns(0);
        _groupServiceMock
            .Setup(service => service.GetGroupByType(GroupType.Guerillas))
            .Returns(new Group(GroupType.Guerillas, 0, 6, "Guerillas", "Guerillas"));

        // Act
        bool isCaught = _escapeService.DoesGuerrillaCatchPlayerEscaping();

        // Assert
        Assert.IsTrue(isCaught);
    }

    [Test]
    public void DoesGuerrillaCatchPlayerEscaping_WhenRandomIsNonZero_ReturnsFalse()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(0, 4))
            .Returns(1);
        _groupServiceMock
            .Setup(service => service.GetGroupByType(GroupType.Guerillas))
            .Returns(new Group(GroupType.Guerillas, 0, 6, "Guerillas", "Guerillas"));

        // Act
        bool isCaught = _escapeService.DoesGuerrillaCatchPlayerEscaping();

        // Assert
        Assert.IsFalse(isCaught);
    }
}
