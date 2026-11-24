using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Decisions;
using Dictator.Core.Models;
using Dictator.Core.Services;

namespace Dictator.ConsoleInterfaces.Tests.Decision;

[TestFixture]
public class AdviceScreenTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IGroupService> _groupServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;
    private AdviceScreen _adviceScreen;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _groupServiceMock = new Mock<IGroupService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();

        _groupServiceMock.Setup(g => g.GetGroupNameByIndex(0)).Returns("The ARMY");
        _groupServiceMock.Setup(g => g.GetGroupNameByIndex(1)).Returns("The PEASANTS");
        _groupServiceMock.Setup(g => g.GetGroupNameByIndex(2)).Returns("The LANDOWNERS");

        _adviceScreen = new AdviceScreen(
            _consoleServiceMock.Object,
            _groupServiceMock.Object,
            _pressAnyKeyControlMock.Object);
    }

    [Test]
    public void Show_WithAudience_DisplaysPopularityStrengthAndFlashingRequesterMarker()
    {
        // Arrange
        var audience = new Audience(
            GroupType.Landowners,
            0,
            4,
            "NNPMMMMM",
            "NNMMMM",
            " CUT SPENDING on the S. POLICE  ");

        // Act
        _adviceScreen.Show(audience);

        // Assert
        _consoleServiceMock.Verify(cs => cs.SetBorderColor(ConsoleColor.DarkBlue), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Clear(), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 2, audience.Text, ConsoleColor.Black, ConsoleColor.DarkYellow), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(22, 6, "+1", ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(22, 7, "+1", ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(22, 8, "+3", ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(24, 8, "< ", ConsoleColor.Magenta, ConsoleColor.Gray), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }
}
