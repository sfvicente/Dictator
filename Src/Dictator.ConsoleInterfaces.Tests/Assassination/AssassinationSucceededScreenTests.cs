using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Assassination;
using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterfaces.Tests.Assassination;

[TestFixture]
public class AssassinationSucceededScreenTests
{
    private AssassinationSucceededScreen _assassinationSucceededScreen;
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;

    [SetUp]
    public void Setup()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();
        _assassinationSucceededScreen = new AssassinationSucceededScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
    }

    [Test]
    public void Show_ClearsConsoleAndWritesMessages()
    {
        // Arrange

        // Act
        _assassinationSucceededScreen.Show();

        // Assert
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 11, "          You're DEAD !         ", ConsoleColor.Gray, ConsoleColor.Black), Times.Once);
        _pressAnyKeyControlMock.Verify(pak => pak.Show(), Times.Once);
    }
}
