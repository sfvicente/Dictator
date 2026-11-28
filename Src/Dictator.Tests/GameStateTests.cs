using System.Linq;
using Dictator.Core.Configuration;
using Dictator.Core.Models;

namespace Dictator.Core.Tests;

[TestFixture]
public class GameStateTests
{
    private GameState _gameState;

    [SetUp]
    public void SetUp()
    {
        _gameState = new GameState(new AccountSettings());
    }

    [Test]
    public void Initialise_PopulatesDefaultAccountAndGameCatalogs()
    {
        Account account = _gameState.GetAccount();
        Audience[] audiences = _gameState.GetAudiences();
        Decision[] decisions = _gameState.GetDecisions();
        News[] news = _gameState.GetNews();

        Assert.AreEqual(1000, account.TreasuryBalance);
        Assert.AreEqual(60, account.MonthlyCosts);
        Assert.IsFalse(account.HasSwissBankAccount);
        Assert.AreEqual(0, account.SwissBankAccountBalance);

        Assert.AreEqual(24, audiences.Length);
        Assert.AreEqual(19, decisions.Length);
        Assert.AreEqual(6, news.Length);
    }

    [Test]
    public void Initialise_ResetsUsedFlagsOnAllCatalogs()
    {
        _gameState.GetAudiences()[0].HasBeenUsed = true;
        _gameState.GetDecisions()[0].HasBeenUsed = true;
        _gameState.GetNews()[0].HasBeenUsed = true;

        _gameState.Initialise();

        Assert.IsFalse(_gameState.GetAudiences().Any(a => a.HasBeenUsed));
        Assert.IsFalse(_gameState.GetDecisions().Any(d => d.HasBeenUsed));
        Assert.IsFalse(_gameState.GetNews().Any(n => n.HasBeenUsed));
    }
}
