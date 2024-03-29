using Dictator.Core.Models;
using Dictator.Core.Services;
using System;

namespace Dictator.Core.Tests.Services;

[TestFixture]
public class NewsServiceTests
{
    private Mock<IRandomService> _randomServiceMock;
    private Mock<IGroupService> _groupServiceMock;
    private Mock<IAccountService> _accountServiceMock;
    private INewsService _newsService;

    [SetUp]
    public void Setup()
    {
        _randomServiceMock = new Mock<IRandomService>();
        _groupServiceMock = new Mock<IGroupService>();
        _accountServiceMock = new Mock<IAccountService>();

        _newsService = new NewsService(_randomServiceMock.Object, _accountServiceMock.Object, _groupServiceMock.Object);
    }

    [Test]
    public void ShouldNewsHappen_ReturnsTrue()
    {
        // Arrange
        _randomServiceMock.Setup(rs => rs.Next(2)).Returns(0);

        // Act
        bool shouldNewsHappen = _newsService.ShouldNewsHappen();

        // Assert
        Assert.IsTrue(shouldNewsHappen);
    }

    [Test]
    public void ShouldNewsHappen_ReturnsFalse()
    {
        // Arrange
        _randomServiceMock.Setup(rs => rs.Next(2)).Returns(1);

        // Act
        bool shouldNewsHappen = _newsService.ShouldNewsHappen();

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
        bool unusedNewsExist = _newsService.DoesUnusedNewsExist(news);

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
        bool unusedNewsExist = _newsService.DoesUnusedNewsExist(news);

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
        _randomServiceMock.Setup(rs => rs.Next(It.IsAny<int>())).Returns(0);

        // Act
        var selectedNews = _newsService.SelectRandomUnusedNews(unusedNews);

        // Assert
        Assert.IsNotNull(selectedNews);
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
        Assert.Throws<InvalidOperationException>(() => _newsService.SelectRandomUnusedNews(usedNews));
    }

    [Test]
    public void ApplyNewsEffects_WithValidNews_CallsServices()
    {
        // Arrange
        var news = new News(10, 5, "PopularityChanges", "StrengthChanges", "Text");

        // Act
        _newsService.ApplyNewsEffects(news);

        // Assert
        _groupServiceMock.Verify(gs => gs.ApplyPopularityChange(It.IsAny<string>()), Times.Once);
        _groupServiceMock.Verify(gs => gs.ApplyStrengthChange(It.IsAny<string>()), Times.Once);
        _accountServiceMock.Verify(asrv => asrv.ApplyTreasuryChanges(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }
}