using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Decisions;
using Dictator.Core.Models;

namespace Dictator.ConsoleInterfaces.Tests.Decision;

[TestFixture]
public class AudienceScreenTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;
    private AudienceScreen _audienceScreen;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();
        _audienceScreen = new AudienceScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
    }

    [Test]
    public void Show_ShouldClearConsole()
    {
        // Arrange
        Audience audience = new(
            GroupType.Army,
            10,
            5,
            "MMLL",
            "PPMM",
            "Some text");

        // Act
        _audienceScreen.Show(audience);

        // Assert
        _consoleServiceMock.Verify(cs => cs.Clear(), Times.Once);
    }

    [Test]
    public void Show_ShouldWriteCorrectLabelsToConsole()
    {
        // Arrange
        Audience audience = new(
            GroupType.Army,
            10,
            5,
            "MMLL",
            "PPMM",
            "Some text");

        // Act
        _audienceScreen.Show(audience);

        // Assert
        _consoleServiceMock.Verify(cs => cs.WriteEmptyLineAt(1, ConsoleColor.Green), Times.Exactly(4));
        _consoleServiceMock.Verify(cs => cs.WriteAt(11, 4, "AN AUDIENCE", ConsoleColor.White, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteEmptyLineAt(5, ConsoleColor.Green), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteEmptyLineAt(It.IsAny<int>(), ConsoleColor.DarkYellow), Times.Exactly(16));
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 11, $" A request from {audience.Requester}", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 15, " Will YOUR EXCELLENCY agree to  ", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 17, $"{audience.Text}", ConsoleColor.Yellow, ConsoleColor.Black), Times.Once);
    }

    [Test]
    public void Show_ShouldInvokePressAnyKeyControlShowMethod()
    {
        // Arrange
        Audience audience = new(
            GroupType.Army,
            10,
            5,
            "MMLL",
            "PPMM",
            "Some text");

        // Act
        _audienceScreen.Show(audience);

        // Assert
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }
}

