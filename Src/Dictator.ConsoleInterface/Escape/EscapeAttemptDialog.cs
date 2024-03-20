using Dictator.ConsoleInterface.Common;
using System;

namespace Dictator.ConsoleInterface.Escape;

public interface IEscapeAttemptDialog
{
    /// <summary>
    ///     Displays the dialog.
    /// </summary>
    /// <returns>The option selected after the dialog has been presented.</returns>
    DialogResult Show();
}

/// <summary>
///     Represents the dialog that is displayed when the choice to escape a war or revolution is presented
///     to the player.
/// </summary>
public class EscapeAttemptDialog : BaseScreen, IEscapeAttemptDialog
{
    private readonly IPressAnyKeyWithYesControl _pressAnyKeyWithYesControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EscapeAttemptDialog"/> class from a <see cref="IPressAnyKeyWithYesControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyWithYesControl">The control that is displayed when the user is required to press a key.</param>
    public EscapeAttemptDialog(IConsoleService consoleService, IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
        : base(consoleService)
    {
        _pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
    }

    /// <summary>
    ///     Displays the dialog.
    /// </summary>
    /// <returns>The option selected after the dialog has been presented.</returns>
    public DialogResult Show()
    {
        _consoleService.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        _consoleService.WriteAt(1, 12, "        ESCAPE ATTEMPT ?        ");

        return _pressAnyKeyWithYesControl.Show();
    }
}
