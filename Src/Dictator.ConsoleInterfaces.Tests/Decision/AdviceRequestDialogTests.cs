using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Decisions;

namespace Dictator.ConsoleInterfaces.Tests.Decision;

[TestFixture]
public class AdviceRequestDialogTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyWithYesControl> _pressAnyKeyWithYesControlMock;
    private AdviceRequestDialog _adviceRequestDialog;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyWithYesControlMock = new Mock<IPressAnyKeyWithYesControl>();
        _adviceRequestDialog = new AdviceRequestDialog(_consoleServiceMock.Object, _pressAnyKeyWithYesControlMock.Object);
    }

    [Test]
    public void Show_ShouldClearConsoleWithGreenColor()
    {
        // Arrange

        // Act
        _adviceRequestDialog.Show();

        // Assert
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Green), Times.Once);
    }

    [Test]
    public void Show_ShouldWriteCorrectLabelsToConsole()
    {
        // Arrange

        // Act
        _adviceRequestDialog.Show();

        // Assert
        _consoleServiceMock.Verify(cs => cs.WriteAt(11, It.IsAny<int>(), "?", ConsoleColor.Black, ConsoleColor.White), Times.Exactly(19));
        _consoleServiceMock.Verify(cs => cs.WriteAt(12, It.IsAny<int>(), " ADVICE ", ConsoleColor.Gray, ConsoleColor.Black), Times.Exactly(19));
        _consoleServiceMock.Verify(cs => cs.WriteAt(20, It.IsAny<int>(), "?", ConsoleColor.Black, ConsoleColor.White), Times.Exactly(19));
    }

    [Test]
    public void Show_ShouldInvokePressAnyKeyWithYesControlShowMethod()
    {
        // Arrange

        // Act
        _adviceRequestDialog.Show();

        // Assert
        _pressAnyKeyWithYesControlMock.Verify(p => p.Show(), Times.Once);
    }
}
