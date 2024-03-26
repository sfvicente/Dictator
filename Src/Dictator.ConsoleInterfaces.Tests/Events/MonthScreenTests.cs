using Dictator.ConsoleInterface.Events;
using Dictator.ConsoleInterface;

namespace Dictator.ConsoleInterfaces.Tests.Events;

[TestFixture]
public class MonthScreenTests
{
    private MonthScreen _monthScreen;
    private Mock<IConsoleService> _consoleServiceMock;

    [SetUp]
    public void Setup()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _monthScreen = new MonthScreen(_consoleServiceMock.Object);
    }

    [Test]
    public void Show_ClearsConsoleAndWritesMonth()
    {
        // Arrange
        int month = 1;

        // Act
        _monthScreen.Show(month);

        // Assert
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Yellow), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(8, 10, "MONTH ", ConsoleColor.Cyan, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(14, 10, $"{month}", ConsoleColor.White, ConsoleColor.Black), Times.Once);
    }
}

