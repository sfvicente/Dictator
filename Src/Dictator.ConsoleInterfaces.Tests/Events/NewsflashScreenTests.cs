using Dictator.ConsoleInterface.Events;
using Dictator.ConsoleInterface;

namespace Dictator.ConsoleInterfaces.Tests.Events;

[TestFixture]
public class NewsflashScreenTests
{
    private NewsflashScreen _newsflashScreen;
    private Mock<IConsoleService> _consoleServiceMock;

    [SetUp]
    public void Setup()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _newsflashScreen = new NewsflashScreen(_consoleServiceMock.Object);
    }

    [Test]
    public void Show_ClearsConsoleAndWritesHeadline()
    {
        // Arrange
        string headline = "Breaking News: Dictatorship Takes Control";

        // Act
        _newsflashScreen.Show(headline);

        // Assert
        _consoleServiceMock.Verify(cs => cs.Clear(ConsoleColor.Gray), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 10, "NEWSFLASH"), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 14, headline), Times.Once);
    }
}
