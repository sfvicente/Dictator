using System;
using System.Linq;

namespace Dictator.Core.Services;

public interface INewsService
{
    public void Initialise();
    public bool ShouldNewsHappen();
    public bool DoesUnusedNewsExist();
    public News SelectRandomUnusedNews();
    public void ApplyNewsEffects(News news);
}

public class NewsService : INewsService
{
    private readonly IRandomService _randomService;
    private readonly IGroupService _groupService;
    private readonly IAccountService _accountService;
    private News[] _news;

    public NewsService(
        IRandomService randomService,
        IAccountService accountService,
        IGroupService groupService)
    {
        _groupService = groupService;
        _randomService = randomService;
        _accountService = accountService;
    }

    public void Initialise()
    {
        _news = [
            new News(0, 0, "MMMMMIMM", "MMMQMI", " PRESIDENT LOSES S.POLICE FILES "),
            new News(0, 0, "MMMMMMMM", "LMMVMM", " CUBANS ARM and TRAIN GUERILLAS "),
            new News(0, 0, "MMMMMMMM", "IMMOMN", "ACCIDENT. ARMY BARRACKS BLOWS UP"),
            new News(0, 0, "MMMMMMMM", "MMJMKM", "   BANANA PRICES FALL by 98%    "),
            new News(0, 0, "MMMMMMMM", "MMOMIM", "  MAJOR EARTHQUAKE in LEFTOTO   "),
            new News(0, 0, "MMMMMMMM", "MILKMM", "A PLAGUE SWEEPS through PEASANTS"),
        ];
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
        News[] unusedNews = _news.Where(x => !x.HasBeenUsed).ToArray();

        return unusedNews;
    }
}
