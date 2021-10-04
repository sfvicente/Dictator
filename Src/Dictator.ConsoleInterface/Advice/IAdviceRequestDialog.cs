using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterface.Advice
{
    /// <summary>
    ///     Represents the dialog that is displayed when a player is requested to select if they want to
    ///     to be given advice on the impacts of a game action on the groups.
    /// </summary>
    public interface IAdviceRequestDialog
    {
        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <returns>The option selected after the dialog has been presented.</returns>
        DialogResult Show();
    }
}
