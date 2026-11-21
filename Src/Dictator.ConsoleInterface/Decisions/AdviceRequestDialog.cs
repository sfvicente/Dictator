using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Decisions;

public interface IAdviceRequestDialog
{
    DialogResult Show();
}

public class AdviceRequestDialog : BaseScreen, IAdviceRequestDialog
{
    private readonly IPressAnyKeyWithYesControl _pressAnyKeyWithYesControl;

    public AdviceRequestDialog(IConsoleService consoleService, IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        : base(consoleService)
    {
        _pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
    }

    public DialogResult Show()
    {
        _consoleService.SetBorderColor(ConsoleColor.DarkBlue);
        _consoleService.Clear(ConsoleColor.DarkGreen);

        for (int row = 2; row < 21; row++)
        {
            _consoleService.SetFlash(true);
            _consoleService.WriteAt(11, row, "?", ConsoleColor.White, ConsoleColor.Black);
            _consoleService.SetFlash(false);
            _consoleService.WriteAt(12, row, " ADVICE ", ConsoleColor.Gray, ConsoleColor.Black);
            _consoleService.SetFlash(true);
            _consoleService.WriteAt(20, row, "?", ConsoleColor.White, ConsoleColor.Black);
            _consoleService.SetFlash(false);
        }

        return _pressAnyKeyWithYesControl.Show();
    }
}
