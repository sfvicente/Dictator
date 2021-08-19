namespace Dictator.ConsoleInterface.Common
{
    public interface IPressAnyKeyWithYesControl
    {
        /// <summary>
        ///     Displays the control.
        /// </summary>
        /// <returns>The option selected after the control has been presented.</returns>
        public DialogResult Show();
    }
}
