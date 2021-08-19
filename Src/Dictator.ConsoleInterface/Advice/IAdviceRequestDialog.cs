using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterface.Advice
{
    public interface IAdviceRequestDialog
    {
        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <returns>The option selected after the dialog has been presented.</returns>
        DialogResult Show();
    }
}
