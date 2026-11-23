using Dictator.ConsoleInterface;
using Dictator.ConsoleInterface.Common;
using Dictator.ConsoleInterface.Start;
using Dictator.Core.Models;

namespace Dictator.ConsoleInterfaces.Tests.Start;

[TestFixture]
public class EndScreenTests
{
    private Mock<IConsoleService> _consoleServiceMock;
    private Mock<IPressAnyKeyControl> _pressAnyKeyControlMock;
    private EndScreen _endScreen;

    [SetUp]
    public void SetUp()
    {
        _consoleServiceMock = new Mock<IConsoleService>();
        _pressAnyKeyControlMock = new Mock<IPressAnyKeyControl>();
        _endScreen = new EndScreen(_consoleServiceMock.Object, _pressAnyKeyControlMock.Object);
    }

    [Test]
    public void Show_WhenPlayerIsAlive_DisplaysStayingAliveAndMoneygrabbingRows()
    {
        // Arrange
        var score = new Score
        {
            TotalPopularity = 35,
            MonthsInOffice = 10,
            PointsForMonthsInOffice = 30,
            PointsForStayingAlive = 10,
            MoneyGrabbed = 500,
            PointsForMoneyGrabbing = 50,
            TotalScore = 125,
            HighestScore = 100
        };

        // Act
        _endScreen.Show(score);

        // Assert
        _consoleServiceMock.Verify(cs => cs.WriteAt(5, 4, "Your RATING as PRESIDENT", ConsoleColor.White, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 10, " For staying ALIVE - ", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(6, 12, "MONEYGRABBING", ConsoleColor.DarkGreen, ConsoleColor.White), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(29, 16, "125", ConsoleColor.Yellow, ConsoleColor.Black), Times.Once);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 18, "[ Highest Score so far is 125 ]", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
        _pressAnyKeyControlMock.Verify(p => p.Show(), Times.Once);
    }

    [Test]
    public void Show_WhenPlayerIsDead_OmitsStayingAliveAndMoneygrabbingRows()
    {
        // Arrange
        var score = new Score
        {
            TotalPopularity = 20,
            MonthsInOffice = 5,
            PointsForMonthsInOffice = 15,
            PointsForStayingAlive = 0,
            MoneyGrabbed = 300,
            PointsForMoneyGrabbing = 30,
            TotalScore = 35,
            HighestScore = 80
        };

        // Act
        _endScreen.Show(score);

        // Assert
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 10, " For staying ALIVE - ", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Never);
        _consoleServiceMock.Verify(cs => cs.WriteAt(6, 12, "MONEYGRABBING", ConsoleColor.DarkGreen, ConsoleColor.White), Times.Never);
        _consoleServiceMock.Verify(cs => cs.WriteAt(1, 18, "[ Highest Score so far is 80 ]", ConsoleColor.DarkYellow, ConsoleColor.Black), Times.Once);
    }
}
