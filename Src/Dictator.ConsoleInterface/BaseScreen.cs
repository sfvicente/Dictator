namespace Dictator.ConsoleInterface;

public abstract class BaseScreen
{
    protected readonly IConsoleService _consoleService;

    protected BaseScreen(IConsoleService consoleService)
    {
        _consoleService = consoleService;
    }
}