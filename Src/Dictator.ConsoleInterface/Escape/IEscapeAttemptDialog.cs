using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterface.Escape
{
    /// <summary>
    ///     Represents the dialog that is displayed when the choice to escape a war or revolution is presented
    ///     to the player.
    /// </summary>
    public interface IEscapeAttemptDialog
    {
        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <returns>The option selected after the dialog has been presented.</returns>
        DialogResult Show();
    }
}
