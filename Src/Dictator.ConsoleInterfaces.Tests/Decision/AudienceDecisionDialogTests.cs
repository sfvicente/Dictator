using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Decisions;

namespace Dictator.ConsoleInterfaces.Tests.Decision;

[TestFixture]
public class AudienceDecisionDialogTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;
    private Mock<IPressAnyKeyWithYesControl> _pressAnyKeyWithYesControlMock;
    private AudienceDecisionDialog _dialog;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();
        _pressAnyKeyWithYesControlMock = new Mock<IPressAnyKeyWithYesControl>();
        _dialog = new AudienceDecisionDialog(
            _consoleServiceMock.Object,
            _pressAnyKeyControlMock.Object,
            _pressAnyKeyWithYesControlMock.Object);
    }

    [Test]
    public void ShowInsufficientFunds_DisplaysForcedNoWarning()
    {
        // Act
        _dialog.ShowInsufficientFunds();

        // Assert
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(2, 11, "You have INSUFFICIENT FUNDS to", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(2, 13, "PAY for this DECISION.", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.Write("NO", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }
}
