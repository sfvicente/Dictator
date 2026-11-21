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
        _consoleService.SetBorderColor(ConsoleColor.DarkBlue);
        _consoleService.Clear(ConsoleColor.Gray);
        _consoleService.WriteAt(12, 11, "NEWS", ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.SetFlash(true);
        _consoleService.WriteAt(16, 11, "FLASH", ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.SetFlash(false);
        _consoleService.WriteAt(1, 14, headline);
        _consoleService.WaitForKeyPressed();
    }
}
