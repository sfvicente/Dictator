using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Revolution;

/// <summary>
///     Represents the dialog that is displayed when the player crushes the revolution
///     and is given a choice to punish the revolutionaries.
/// </summary>
public interface IRevolutionCrushedDialog
{
    public DialogResult Show();
}

/// <summary>
///     Represents the dialog that is displayed when the player crushes the revolution
///     and is given a choice to punish the revolutionaries.
/// </summary>
public class RevolutionCrushedDialog : BaseScreen, IRevolutionCrushedDialog
{
    private readonly IPressAnyKeyWithYesControl _pressAnyKeyWithYesControl;

    public RevolutionCrushedDialog(IConsoleService consoleService, IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        : base(consoleService)
    {
        _pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
    }

    public DialogResult Show()
    {
        _consoleService.SetBorderColor(ConsoleColor.Black);
        _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.SetFlash(true);
        _consoleService.WriteAt(1, 10, "  The REVOLT has been CRUSHED   ");
        _consoleService.SetFlash(false);
        _consoleService.WriteAt(1, 12, "\u008F\u008FPUNISH the REVOLUTIONARIES ?\u008F\u008F");

        return _pressAnyKeyWithYesControl.Show();
    }
}
