using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Assassination;
using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterfaces.Tests.Assassination;

[TestFixture]
public class AssassinationSuccededScreenTests
{
    private AssassinationSuccededScreen _assassinationSuccededScreen;
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;

    [SetUp]
    public void Setup()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();
        _assassinationSuccededScreen = new AssassinationSuccededScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
    }

    [Test]
    public void Show_ClearsConsoleAndWritesMessages()
    {
        // Arrange

        // Act
        _assassinationSuccededScreen.Show();

        // Assert
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 11, "          You're DEAD !         ", ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _pressAnyKeyControlMock.Verify(pak => pak.Show(), Times.Once);
    }
}
