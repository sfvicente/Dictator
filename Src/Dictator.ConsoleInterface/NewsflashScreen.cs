using System;

namespace Dictator.ConsoleInterface;

public interface INewsflashScreen
{
    public void Show(string headline);
}

public class NewsflashScreen : INewsflashScreen
{
    public void Show(string headline)
    {
        ConsoleEx.Clear(ConsoleColor.Gray);
        ConsoleEx.WriteAt(1, 10, "NEWSFLASH");
        ConsoleEx.WriteAt(1, 14, headline);
        Console.ReadKey(true);
    }
}
