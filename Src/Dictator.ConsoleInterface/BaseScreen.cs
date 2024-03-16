namespace Dictator.ConsoleInterface;

public class BaseScreen
{
    protected readonly IConsoleService _consoleService;

    public BaseScreen(IConsoleService consoleService)
    {
        _consoleService = consoleService;
    }

    public virtual void Show()
    {
        _consoleService.Clear();
    }
}