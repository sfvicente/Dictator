using Dictator.ConsoleInterface;

namespace Dictator.Web.Services;

public class WebSpectrumConsoleService : IConsoleService
{
    public const int Columns = 32;
    public const int Rows = 24;

    private readonly SpectrumCell[,] _cells = new SpectrumCell[Rows, Columns];
    private int _cursorLeft = 1;
    private int _cursorTop = 1;
    private ConsoleColor _backgroundColor = ConsoleColor.Black;
    private ConsoleColor _foregroundColor = ConsoleColor.White;
    private ConsoleColor _borderColor = ConsoleColor.DarkBlue;
    private bool _flash = false;

    private TaskCompletionSource<ConsoleKey>? _keyTcs;
    private readonly Queue<ConsoleKey> _keyQueue = new();

    public event Action? OnScreenUpdated;
    public event Action<bool>? OnWaitingForKeyChanged;

    public ConsoleColor BorderColor => _borderColor;
    public bool IsWaitingForKey => _keyTcs != null;

    public WebSpectrumConsoleService()
    {
        Clear(ConsoleColor.Black, ConsoleColor.White);
    }

    public SpectrumCell[,] GetBufferSnapshot()
    {
        return (SpectrumCell[,])_cells.Clone();
    }

    public int[] GetSerializedBuffer()
    {
        // Pack each cell into an int: charCode | (bg << 16) | (fg << 20) | (flash << 24)
        int[] packed = new int[Rows * Columns];
        int index = 0;

        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                SpectrumCell cell = _cells[r, c];
                int charCode = cell.Character;
                int bg = (int)cell.Background & 0x0F;
                int fg = (int)cell.Foreground & 0x0F;
                int fl = cell.Flash ? 1 : 0;
                packed[index++] = charCode | (bg << 16) | (fg << 20) | (fl << 24);
            }
        }

        return packed;
    }

    public void SubmitKey(ConsoleKey key)
    {
        if (_keyTcs != null)
        {
            var tcs = _keyTcs;
            _keyTcs = null;
            OnWaitingForKeyChanged?.Invoke(false);
            tcs.TrySetResult(key);
        }
        else
        {
            _keyQueue.Enqueue(key);
        }
    }

    public Task<ConsoleKey> WaitForKeyAsync()
    {
        if (_keyQueue.Count > 0)
        {
            return Task.FromResult(_keyQueue.Dequeue());
        }

        _keyTcs = new TaskCompletionSource<ConsoleKey>(TaskCreationOptions.RunContinuationsAsynchronously);
        OnWaitingForKeyChanged?.Invoke(true);
        OnScreenUpdated?.Invoke();
        return _keyTcs.Task;
    }

    public void Initialise()
    {
        _borderColor = ConsoleColor.DarkBlue;
        Clear(ConsoleColor.Black, ConsoleColor.White);
    }

    public void Write(string text, ConsoleColor foregroundColor)
    {
        _foregroundColor = foregroundColor;
        WriteInternal(text);
    }

    public void Write(string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
    {
        ConsoleColor prevBg = _backgroundColor;
        ConsoleColor prevFg = _foregroundColor;

        _backgroundColor = backgroundColor;
        _foregroundColor = foregroundColor;
        WriteInternal(text);
        _backgroundColor = prevBg;
        _foregroundColor = prevFg;
    }

    public void WriteEmptyLineAt(int top)
    {
        WriteAt(1, top, new string(' ', Columns));
    }

    public void WriteEmptyLineAt(int top, ConsoleColor backgroundColor)
    {
        ConsoleColor prevBg = _backgroundColor;
        _backgroundColor = backgroundColor;
        WriteAt(1, top, new string(' ', Columns));
        _backgroundColor = prevBg;
    }

    public void WriteAt(int left, int top, string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
    {
        ConsoleColor prevBg = _backgroundColor;
        ConsoleColor prevFg = _foregroundColor;

        _backgroundColor = backgroundColor;
        _foregroundColor = foregroundColor;
        WriteAt(left, top, text);
        _backgroundColor = prevBg;
        _foregroundColor = prevFg;
    }

    public void WriteAt(int left, int top, string text, ConsoleColor foregroundColor)
    {
        _foregroundColor = foregroundColor;
        WriteAt(left, top, text);
    }

    public void SetCursorPosition(int left, int top)
    {
        _cursorLeft = Math.Clamp(left, 1, Columns);
        _cursorTop = Math.Clamp(top, 1, Rows);
    }

    public void WriteAt(int left, int top, string text)
    {
        SetCursorPosition(left, top);
        WriteInternal(text);
    }

    public void WriteCenteredAt(int top, string text)
    {
        int left = Math.Max(1, (Columns - text.Length) / 2);
        WriteAt(left, top, text);
    }

    public void PrintAt(int row, int column, string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
    {
        ConsoleColor prevBg = _backgroundColor;
        ConsoleColor prevFg = _foregroundColor;

        _backgroundColor = backgroundColor;
        _foregroundColor = foregroundColor;
        PrintAt(row, column, text);
        _backgroundColor = prevBg;
        _foregroundColor = prevFg;
    }

    public void PrintAt(int row, int column, string text)
    {
        SetCursorAt(row, column);
        WriteInternal(text);
    }

    public void SetCursorAt(int row, int column)
    {
        // PrintAt in ZX Spectrum BASIC is 0-indexed (row 0..23, col 0..31); convert to 1-indexed
        _cursorTop = Math.Clamp(row + 1, 1, Rows);
        _cursorLeft = Math.Clamp(column + 1, 1, Columns);
    }

    public void Clear()
    {
        Clear(' ');
    }

    public void Clear(char character)
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                _cells[r, c] = new SpectrumCell(character, _backgroundColor, _foregroundColor, false);
            }
        }
        _cursorLeft = 1;
        _cursorTop = 1;
        OnScreenUpdated?.Invoke();
    }

    public void Clear(ConsoleColor backgroundColor)
    {
        ConsoleColor prevBg = _backgroundColor;
        _backgroundColor = backgroundColor;
        Clear(' ');
        _backgroundColor = prevBg;
    }

    public void Clear(ConsoleColor backgroundColor, ConsoleColor foregroundColor)
    {
        _backgroundColor = backgroundColor;
        _foregroundColor = foregroundColor;
        Clear(' ');
    }

    public void Clear(char character, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
    {
        _backgroundColor = backgroundColor;
        _foregroundColor = foregroundColor;
        Clear(character);
    }

    public void WaitForKeyPressed()
    {
        // In WebAssembly, key waiting is handled asynchronously by WebGameRunner.WaitForKeyAsync().
    }

    public ConsoleKey ReadKey()
    {
        // In WebAssembly, key reading is handled asynchronously by WebGameRunner.WaitForKeyAsync().
        return ConsoleKey.Enter;
    }

    public void SetBackgroundColor(ConsoleColor backgroundColor)
    {
        _backgroundColor = backgroundColor;
    }

    public void SetForegroundColor(ConsoleColor foregroundColor)
    {
        _foregroundColor = foregroundColor;
    }

    public void SetBorderColor(ConsoleColor borderColor)
    {
        _borderColor = borderColor;
        OnScreenUpdated?.Invoke();
    }

    public void SetFlash(bool flash)
    {
        _flash = flash;
    }

    private void WriteInternal(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        int row = _cursorTop - 1;
        int col = _cursorLeft - 1;

        foreach (char ch in text)
        {
            if (row >= 0 && row < Rows && col >= 0 && col < Columns)
            {
                _cells[row, col] = new SpectrumCell(ch, _backgroundColor, _foregroundColor, _flash);
            }

            col++;
            if (col >= Columns)
            {
                col = 0;
                row++;
                if (row >= Rows)
                {
                    row = Rows - 1;
                }
            }
        }

        _cursorTop = row + 1;
        _cursorLeft = col + 1;
        OnScreenUpdated?.Invoke();
    }
}
