using Dictator.Core.Models;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player is given a list of options to make a
    ///     presidential decision.
    /// </summary>
    public interface IPresidentialDecisionMainDialog
    {
        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <returns>The type of presidential decision that has been selected.</returns>
        public DecisionType Show();
    }
}
