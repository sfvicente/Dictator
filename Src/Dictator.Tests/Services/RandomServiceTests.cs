using Dictator.Core.Services;

namespace Dictator.Tests.Services;

[TestFixture]
public class RandomServiceTests
{
    private RandomService _randomService;

    [SetUp]
    public void Setup()
    {
        _randomService = new RandomService();
    }

    [Test]
    public void Next_MaxValue_ReturnsValueLessThanMax()
    {
        int maxValue = 100;
        int result = _randomService.Next(maxValue);
        Assert.That(result, Is.LessThan(maxValue));
    }

    [Test]
    public void Next_MinMaxValues_ReturnsValueInRange()
    {
        int minValue = 50;
        int maxValue = 100;
        int result = _randomService.Next(minValue, maxValue);
        Assert.That(result, Is.GreaterThanOrEqualTo(minValue));
        Assert.That(result, Is.LessThan(maxValue));
    }
}