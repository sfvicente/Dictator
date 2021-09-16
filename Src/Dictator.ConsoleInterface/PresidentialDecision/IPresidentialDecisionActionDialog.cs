using Dictator.ConsoleInterface.Common;
using Dictator.Core;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player needs to make a decision to accept or refuse
    ///     the presidential option that has been selected.
    /// </summary>
    public interface IPresidentialDecisionActionDialog
    {
        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <param name="decision">The decision to be displayed to the player for acceptance or refusal.</param>
        /// <returns>The option selected after the dialog has been presented.</returns>
        DialogResult Show(Decision decision);
    }
}
