using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterface.Escape
{
    public interface IEscapeAttemptDialog
    {
        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <returns>The option selected after the dialog has been presented.</returns>
        DialogResult Show();
    }
}
