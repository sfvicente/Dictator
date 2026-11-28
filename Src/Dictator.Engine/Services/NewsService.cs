using System;
using System.Linq;
using Dictator.Core.Models;

namespace Dictator.Core.Services;

public interface INewsService
{
    public bool ShouldNewsHappen();
    public bool DoesUnusedNewsExist();
    public News SelectRandomUnusedNews();
    public void ApplyNewsEffects(News news);
}

public class NewsService : INewsService
{
    private readonly IGameState _gameState;
    private readonly IRandomService _randomService;
    private readonly IGroupService _groupService;
    private readonly IAccountService _accountService;

    public NewsService(
        IGameState gameState,
        IRandomService randomService,
        IAccountService accountService,
        IGroupService groupService)
    {
        _gameState = gameState;
        _groupService = groupService;
        _randomService = randomService;
        _accountService = accountService;
    }

    public bool ShouldNewsHappen()
    {
        int number = _randomService.Next(2);

        if (number == 0)
        {
            return true;
        }

        return false;
    }

    public bool DoesUnusedNewsExist()
    {
        News[] unusedNews = GetUnusedNews();

        if (unusedNews.Length != 0)
        {
            return true;
        }

        return false;
    }

    public News SelectRandomUnusedNews()
    {
        News[] unusedNews = GetUnusedNews();

        if (unusedNews.Length != 0)
        {
            var randomUnusedNews = unusedNews.ElementAt(_randomService.Next(unusedNews.Length));

            randomUnusedNews.HasBeenUsed = true;

            return randomUnusedNews;
        }

        throw new InvalidOperationException("There are unused news items in the collection.");
    }

    public void ApplyNewsEffects(News news)
    {
        ArgumentNullException.ThrowIfNull(news);

        _groupService.ApplyPopularityChange(news.GroupPopularityChanges);
        _groupService.ApplyStrengthChange(news.GroupStrengthChanges);
        _accountService.ApplyTreasuryChanges(news.Cost, news.MonthlyCost);
    }

    private News[] GetUnusedNews()
    {
        News[] news = _gameState.GetNews();
        News[] unusedNews = news.Where(x => !x.HasBeenUsed).ToArray();

        return unusedNews;
    }
}
