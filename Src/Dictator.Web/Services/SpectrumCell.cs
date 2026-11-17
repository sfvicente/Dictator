namespace Dictator.Web;

public readonly record struct SpectrumCell(
    char Character,
    ConsoleColor Background,
    ConsoleColor Foreground,
    bool Flash);
