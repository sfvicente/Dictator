using Dictator.Core.Models;
using Dictator.Core.Services;
using System;

namespace Dictator.Core.Tests.Services;

[TestFixture]
public class NewsServiceTests
{
    private Mock<IRandomService> randomServiceMock;
    private Mock<IGroupService> groupServiceMock;
    private Mock<IAccountService> accountServiceMock;
    private INewsService newsService;

    [SetUp]
    public void Setup()
    {
        randomServiceMock = new Mock<IRandomService>();
        groupServiceMock = new Mock<IGroupService>();
        accountServiceMock = new Mock<IAccountService>();

        newsService = new NewsService(randomServiceMock.Object, accountServiceMock.Object, groupServiceMock.Object);
    }

    [Test]
    public void ShouldNewsHappen_ReturnsTrue()
    {
        // Arrange
        randomServiceMock.Setup(rs => rs.Next(2)).Returns(0);

        // Act
        bool shouldNewsHappen = newsService.ShouldNewsHappen();

        // Assert
        Assert.IsTrue(shouldNewsHappen);
    }

    [Test]
    public void ShouldNewsHappen_ReturnsFalse()
    {
        // Arrange
        randomServiceMock.Setup(rs => rs.Next(2)).Returns(1);

        // Act
        bool shouldNewsHappen = newsService.ShouldNewsHappen();

        // Assert
        Assert.IsFalse(shouldNewsHappen);
    }

    [Test]
    public void DoesUnusedNewsExist_WithUnusedNews_ReturnsTrue()
    {
        // Arrange
        var news = new[]
        {
            new News(10, 5, "PopularityChanges1", "StrengthChanges1", "Text1"),
            new News(20, 10, "PopularityChanges2", "StrengthChanges2", "Text2"),
            new News(30, 15, "PopularityChanges3", "StrengthChanges3", "Text3")
        };

        // Act
        bool unusedNewsExist = newsService.DoesUnusedNewsExist(news);

        // Assert
        Assert.IsTrue(unusedNewsExist);
    }

    [Test]
    public void DoesUnusedNewsExist_WithNoUnusedNews_ReturnsFalse()
    {
        // Arrange
        var news = new[]
        {
            new News(10, 5, "PopularityChanges1", "StrengthChanges1", "Text1") { HasBeenUsed = true },
            new News(20, 10, "PopularityChanges2", "StrengthChanges2", "Text2") { HasBeenUsed = true },
            new News(30, 15, "PopularityChanges3", "StrengthChanges3", "Text3") { HasBeenUsed = true }
        };

        // Act
        bool unusedNewsExist = newsService.DoesUnusedNewsExist(news);

        // Assert
        Assert.IsFalse(unusedNewsExist);
    }

    [Test]
    public void SelectRandomUnusedNews_WithUnusedNews_ReturnsUnusedNews()
    {
        // Arrange
        var unusedNews = new[]
        {
            new News(10, 5, "PopularityChanges1", "StrengthChanges1", "Text1"),
            new News(20, 10, "PopularityChanges2", "StrengthChanges2", "Text2"),
            new News(30, 15, "PopularityChanges3", "StrengthChanges3", "Text3")
        };
        randomServiceMock.Setup(rs => rs.Next(It.IsAny<int>())).Returns(0);

        // Act
        var selectedNews = newsService.SelectRandomUnusedNews(unusedNews);

        // Assert
        Assert.IsNotNull(selectedNews);
        Assert.IsFalse(selectedNews.HasBeenUsed);
    }

    [Test]
    public void SelectRandomUnusedNews_NoUnusedNews_ThrowsException()
    {
        // Arrange
        var usedNews = new[]
        {
            new News(10, 5, "PopularityChanges1", "StrengthChanges1", "Text1") { HasBeenUsed = true },
            new News(20, 10, "PopularityChanges2", "StrengthChanges2", "Text2") { HasBeenUsed = true },
            new News(30, 15, "PopularityChanges3", "StrengthChanges3", "Text3") { HasBeenUsed = true }
        };

        // Act and Assert
        Assert.Throws<InvalidOperationException>(() => newsService.SelectRandomUnusedNews(usedNews));
    }

    [Test]
    public void ApplyNewsEffects_WithValidNews_CallsServices()
    {
        // Arrange
        var news = new News(10, 5, "PopularityChanges", "StrengthChanges", "Text");

        // Act
        newsService.ApplyNewsEffects(news);

        // Assert
        groupServiceMock.Verify(gs => gs.ApplyPopularityChange(It.IsAny<string>()), Times.Once);
        groupServiceMock.Verify(gs => gs.ApplyStrengthChange(It.IsAny<string>()), Times.Once);
        accountServiceMock.Verify(asrv => asrv.ApplyTreasuryChanges(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }
}