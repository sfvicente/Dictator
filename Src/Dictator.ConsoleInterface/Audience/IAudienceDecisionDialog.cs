using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterface.Audience
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player needs to decide on
    ///     the decision of the petition by one of the groups. It includes the treasury
    ///     costs involved.
    /// </summary>
    public interface IAudienceDecisionDialog
    {
        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <param name="audience">The audience information to display on the screen.</param>
        /// <returns>The option selected after the dialog has been presented.</returns>
        DialogResult Show(Core.Audience audience);
    }
}
