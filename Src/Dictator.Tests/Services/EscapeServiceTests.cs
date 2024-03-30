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
    public void IsPlayerAbleToEscapeAfterLosingWar_ReturnsTrueOrFalse()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(3))
            .Returns(0); // Ensures always returns true

        // Act
        bool hasEscaped = _escapeService.IsPlayerAbleToEscapeAfterLosingWar();

        // Assert
        Assert.IsTrue(hasEscaped); // The player should be able to escape
    }

    [Test]
    public void IsPlayerAbleToEscapeByHelicopter_ReturnsTrueOrFalse()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(4))
            .Returns(1); // Ensures always returns true

        // Act
        bool isEscaped = _escapeService.IsPlayerAbleToEscapeByHelicopter();

        // Assert
        Assert.IsTrue(isEscaped); // The player should be able to escape by helicopter
    }

    [Test]
    public void DoesGuerrillaCatchPlayerEscaping_ReturnsTrueOrFalse()
    {
        // Arrange
        _randomServiceMock
            .Setup(service => service.Next(0, It.IsAny<int>()))
            .Returns(0); // Ensures always returns true
        _groupServiceMock
            .Setup(service => service.GetGroupByType(GroupType.Guerillas))
            .Returns(new Group(GroupType.Guerillas, It.IsAny<int>(), 6, "Guerillas", "Guerillas")); // Assuming strength for testing

        // Act
        bool isCaught = _escapeService.DoesGuerrillaCatchPlayerEscaping();

        // Assert
        Assert.IsTrue(isCaught); // The player should be caught by the guerrillas
    }
}
