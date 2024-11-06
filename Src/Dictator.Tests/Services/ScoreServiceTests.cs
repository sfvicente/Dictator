using Dictator.Core.Models;
using Dictator.Core.Services;

namespace Dictator.Core.Tests.Services;

[TestFixture]
public class ScoreServiceTests
{
    private Mock<IGroupService> _groupServiceMock;
    private Mock<IGovernmentService> _governmentServiceMock;
    private Mock<IAccountService> _accountServiceMock;
    private ScoreService _scoreService;

    [SetUp]
    public void SetUp()
    {
        _groupServiceMock = new Mock<IGroupService>();
        _governmentServiceMock = new Mock<IGovernmentService>();
        _accountServiceMock = new Mock<IAccountService>();

        _scoreService = new ScoreService(_accountServiceMock.Object, _groupServiceMock.Object, _governmentServiceMock.Object);
    }

    [Test]
    public void GetCurrentScore_ShouldReturnCorrectScore()
    {
        // Arrange
        _groupServiceMock.Setup(g => g.GetTotalPopularity()).Returns(50);
        _governmentServiceMock.Setup(g => g.GetMonth()).Returns(12);
        _accountServiceMock.Setup(a => a.GetSwissBankAccountBalance()).Returns(1000);
        _governmentServiceMock.Setup(g => g.GetLastScore()).Returns(200);
        _governmentServiceMock.Setup(g => g.IsPlayerAlive()).Returns(true);

        // Act
        Score score = _scoreService.GetCurrentScore();

        // Assert
        Assert.AreEqual(50, score.TotalPopularity);
        Assert.AreEqual(12, score.MonthsInOffice);
        Assert.AreEqual(36, score.PointsForMonthsInOffice);  // 12 * 3
        Assert.AreEqual(1000, score.MoneyGrabbed);
        Assert.AreEqual(100, score.PointsForMoneyGrabbing);  // 1000 / 10
        Assert.AreEqual(200, score.HighestScore);
        Assert.AreEqual(10, score.PointsForStayingAlive);
        Assert.AreEqual(50 + 36 + 100 + 10, score.TotalScore);
    }

    [Test]
    public void GetCurrentHighScore_ShouldReturnLastScoreFromGovernmentService()
    {
        // Arrange
        int lastHighScore = 250;
        _governmentServiceMock.Setup(g => g.GetLastScore()).Returns(lastHighScore);

        // Act
        int highScore = _scoreService.GetCurrentHighScore();

        // Assert
        Assert.AreEqual(lastHighScore, highScore);
    }

    [Test]
    public void SaveHighScore_ShouldSetNewHighScore_WhenCurrentScoreIsHigher()
    {
        // Arrange
        _groupServiceMock.Setup(g => g.GetTotalPopularity()).Returns(100);
        _governmentServiceMock.Setup(g => g.GetMonth()).Returns(10);
        _accountServiceMock.Setup(a => a.GetSwissBankAccountBalance()).Returns(500);
        _governmentServiceMock.Setup(g => g.GetLastScore()).Returns(150); // Existing high score
        _governmentServiceMock.Setup(g => g.IsPlayerAlive()).Returns(true);

        // Act
        _scoreService.SaveHighScore();

        // Assert
        int expectedNewHighScore = 100 + (10 * 3) + (500 / 10) + 10;
        _governmentServiceMock.Verify(g => g.SetHighScore(expectedNewHighScore), Times.Once);
    }

    [Test]
    public void SaveHighScore_ShouldNotSetNewHighScore_WhenCurrentScoreIsNotHigher()
    {
        // Arrange
        _groupServiceMock.Setup(g => g.GetTotalPopularity()).Returns(50);
        _governmentServiceMock.Setup(g => g.GetMonth()).Returns(10);
        _accountServiceMock.Setup(a => a.GetSwissBankAccountBalance()).Returns(500);
        _governmentServiceMock.Setup(g => g.GetLastScore()).Returns(200); // Higher existing high score
        _governmentServiceMock.Setup(g => g.IsPlayerAlive()).Returns(true);

        // Act
        _scoreService.SaveHighScore();

        // Assert
        _governmentServiceMock.Verify(g => g.SetHighScore(It.IsAny<int>()), Times.Never);
    }
}
