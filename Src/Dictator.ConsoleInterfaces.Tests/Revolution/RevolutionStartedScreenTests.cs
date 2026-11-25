using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Revolution;
using Dictator.Core.Models;

namespace Dictator.ConsoleInterfaces.Tests.Revolution;

[TestFixture]
public class RevolutionStartedScreenTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;
    private RevolutionStartedScreen _revolutionStartedScreen;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();
        _revolutionStartedScreen = new RevolutionStartedScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
    }

    [Test]
    public void Show_WithRevolutionStateAndAlly_DisplaysAllThreeStrengthLinesAndHeader()
    {
        // Arrange
        var state = new RevolutionState
        {
            PlayerStrength = 4,
            AllyName = "The ARMY",
            AllyStrength = 6,
            RevolutionaryStrength = 12
        };

        // Act
        _revolutionStartedScreen.Show(state);

        // Assert
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 9, "YOUR STRENGTH is 4"), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 11, "The ARMY strength is 6"), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 13, "The REVOLUTIONARY'S is 12"), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 17, "   The REVOLUTION has STARTED   "), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void Show_WithRevolutionStateWithoutAlly_OmitsAllyStrengthLine()
    {
        // Arrange
        var state = new RevolutionState
        {
            PlayerStrength = 4,
            AllyName = null,
            AllyStrength = 0,
            RevolutionaryStrength = 11
        };

        // Act
        _revolutionStartedScreen.Show(state);

        // Assert
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 9, "YOUR STRENGTH is 4"), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 11, It.IsAny<string>()), Times.Never);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 13, "The REVOLUTIONARY'S is 11"), Times.Once);
    }
}
