using Dictator.ConsoleInterface.Assassination;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface;

namespace Dictator.ConsoleInterfaces.Tests.Assassination;

[TestFixture]
public class AssassinationScreenTests
{
    private AssassinationScreen _assassinationScreen;
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;

    [SetUp]
    public void Setup()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();
        _assassinationScreen = new AssassinationScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
    }

    [Test]
    public void Show_ClearsConsoleAndWritesMessages()
    {
        // Arrange
        string groupName = "The Group";

        // Act
        _assassinationScreen.Show(groupName);

        // Assert
        _consoleServiceMock.Verify(cs => cs.Clear(), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 11, "      ASSASSINATION ATTEMPT     "), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteCenteredAt(12, $"by one of {groupName}"), Times.Once);
        _pressAnyKeyControlMock.Verify(pak => pak.Show(), Times.Once);
    }
}
