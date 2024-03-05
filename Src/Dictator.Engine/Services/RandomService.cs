using System;

namespace Dictator.Core.Services;

public interface IRandomService
{
    int Next(int maxValue);
    int Next(int minValue, int maxValue);
}

public class RandomService : IRandomService
{
    private readonly Random _random;

    public RandomService()
    {
        _random = new Random();
    }

    public int Next(int maxValue)
    {
        return _random.Next(maxValue);
    }

    public int Next(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }
}