namespace Dictator.ConsoleInterface.Common;

/// <summary>
///     Represents the control that is displayed with a panel when the user is required
///     to press a key or select yes.
/// </summary>
public interface IPressAnyKeyWithYesControl
{
    /// <summary>
    ///     Displays the control.
    /// </summary>
    /// <returns>The option selected after the control has been presented.</returns>
    public DialogResult Show();
}
