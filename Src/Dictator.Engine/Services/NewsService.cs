using System;
using System.Linq;

namespace Dictator.Core.Services;

public interface INewsService
{
    /// <summary>
    ///     Initializes the news data.
    /// </summary>
    public void Initialise();

    /// <summary>
    ///     Determines if a random news event should happen in the current month.
    /// </summary>
    /// <returns><c>true</c> if a random news event should happen in the current month; otherwise, <c>false</c>.</returns>
    public bool ShouldNewsHappen();

    /// <summary>
    ///     Determines if at least one news event exists that hasn't been used in the current game.
    /// </summary>
    /// <returns><c>true</c> if an unused news event exists; otherwise, <c>false</c>.</returns>
    public bool DoesUnusedNewsExist();

    /// <summary>
    ///     Selects a random news event that hasn't been used in the current game.
    /// </summary>
    /// <returns>A random unused news event.</returns>
    public News SelectRandomUnusedNews();

    /// <summary>
    ///     Applies the effects of a specific news event on the groups and treasury-
    /// </summary>
    /// <param name="news">The news whose effect will be applied.</param>
    public void ApplyNewsEffects(News news);
}

/// <summary>
///     Provides functionality related to news events that happen at random and impact groups
///     and treasury.
/// </summary>
public class NewsService : INewsService
{
    private readonly IRandomService _randomService;
    private readonly IGroupService groupService;
    private readonly IAccountService accountService;
    private News[] news;

    public NewsService(
        IRandomService randomService,
        IAccountService accountService,
        IGroupService groupService)
    {
        this.groupService = groupService;
        _randomService = randomService;
        this.accountService = accountService;
    }

    /// <summary>
    ///     Initializes the news data.
    /// </summary>
    public void Initialise()
    {
        news = new News[]
        {
            new News(0, 0, "MMMMMIMM", "MMMQMI", " PRESIDENT LOSES S.POLICE FILES "),
            new News(0, 0, "MMMMMMMM", "LMMVMM", " CUBANS ARM and TRAIN GUERILLAS "),
            new News(0, 0, "MMMMMMMM", "IMMOMN", "ACCIDENT. ARMY BARRACKS BLOWS UP"),
            new News(0, 0, "MMMMMMMM", "MMJMKM", "   BANANA PRICES FALL by 98%    "),
            new News(0, 0, "MMMMMMMM", "MMOMIM", "  MAJOR EARTHQUAKE in LEFTOTO   "),
            new News(0, 0, "MMMMMMMM", "MILKMM", "A PLAGUE SWEEPS through PEASANTS"),
        };
    }

    /// <summary>
    ///     Determines if a random news event should happen in the current month.
    /// </summary>
    /// <returns><c>true</c> if a random news event should happen in the current month; otherwise, <c>false</c>.</returns>
    public bool ShouldNewsHappen()
    {
        int number = _randomService.Next(2);

        if (number == 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Determines if at least one news event exists that hasn't been used in the current game.
    /// </summary>
    /// <returns><c>true</c> if an unused news event exists; otherwise, <c>false</c>.</returns>
    public bool DoesUnusedNewsExist()
    {
        News[] unusedNews = GetUnusedNews();

        if (unusedNews.Any())
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Selects a random news event that hasn't been used in the current game.
    /// </summary>
    /// <returns>A random unused news event.</returns>
    public News SelectRandomUnusedNews()
    {
        News[] unusedNews = GetUnusedNews();

        if (unusedNews.Any())
        {
            var randomUnusedNews = unusedNews.ElementAt(_randomService.Next(unusedNews.Count()));

            randomUnusedNews.HasBeenUsed = true;

            return randomUnusedNews;
        }

        throw new InvalidOperationException("There are unused news items in the collection.");
    }

    /// <summary>
    ///     Applies the effects of a specific news event on the groups and treasury-
    /// </summary>
    /// <param name="news">The news whose effect will be applied.</param>
    public void ApplyNewsEffects(News news)
    {
        if (news == null)
        {
            throw new ArgumentNullException();
        }

        groupService.ApplyPopularityChange(news.GroupPopularityChanges);
        groupService.ApplyStrengthChange(news.GroupStrengthChanges);
        accountService.ApplyTreasuryChanges(news.Cost, news.MonthlyCost);
    }

    private News[] GetUnusedNews()
    {
        News[] unusedNews = news.Where(x => !x.HasBeenUsed).ToArray();

        return unusedNews;
    }
}
