using System;
using System.Linq;
using Dictator.Core.Models;

namespace Dictator.Core.Services;

public interface INewsService
{
    public bool ShouldNewsHappen();
    public bool DoesUnusedNewsExist(News[] news);
    public News SelectRandomUnusedNews(News[] news);
    public void ApplyNewsEffects(News news);
}

public class NewsService : INewsService
{
    private readonly IRandomService _randomService;
    private readonly IGroupService _groupService;
    private readonly IAccountService _accountService;

    public NewsService(
        IRandomService randomService,
        IAccountService accountService,
        IGroupService groupService)
    {
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

    public bool DoesUnusedNewsExist(News[] news)
    {
        News[] unusedNews = GetUnusedNews(news);

        if (unusedNews.Length != 0)
        {
            return true;
        }

        return false;
    }

    public News SelectRandomUnusedNews(News[] news)
    {
        News[] unusedNews = GetUnusedNews(news);

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

    private News[] GetUnusedNews(News[] news)
    {
        News[] unusedNews = news.Where(x => !x.HasBeenUsed).ToArray();

        return unusedNews;
    }
}
