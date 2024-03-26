using System;

namespace Dictator.ConsoleInterface.Events;

public interface INewsflashScreen
{
    public void Show(string headline);
}

public class NewsflashScreen : BaseScreen, INewsflashScreen
{
    public NewsflashScreen(IConsoleService consoleService)
        : base(consoleService)
    {

    }

    public void Show(string headline)
    {
        _consoleService.Clear(ConsoleColor.Gray);
        _consoleService.WriteAt(1, 10, "NEWSFLASH");
        _consoleService.WriteAt(1, 14, headline);
        _consoleService.WaitForKeyPressed();
    }
}
