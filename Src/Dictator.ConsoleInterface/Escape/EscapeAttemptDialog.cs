using Dictator.Common;
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
public class EscapeAttemptDialog : IEscapeAttemptDialog
{
    private readonly IPressAnyKeyWithYesControl pressAnyKeyWithYesControl;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EscapeAttemptDialog"/> class from a <see cref="IPressAnyKeyWithYesControl"/>
    ///     component.
    /// </summary>
    /// <param name="pressAnyKeyWithYesControl">The control that is displayed when the user is required to press a key.</param>
    public EscapeAttemptDialog(IPressAnyKeyWithYesControl pressAnyKeyWithYesControl)
    {
        this.pressAnyKeyWithYesControl = pressAnyKeyWithYesControl;
    }

    /// <summary>
    ///     Displays the dialog.
    /// </summary>
    /// <returns>The option selected after the dialog has been presented.</returns>
    public DialogResult Show()
    {
        ConsoleEx.Clear(ConsoleColor.Gray, ConsoleColor.Black);
        ConsoleEx.WriteAt(1, 12, "        ESCAPE ATTEMPT ?        ");

        return pressAnyKeyWithYesControl.Show();
    }
}
