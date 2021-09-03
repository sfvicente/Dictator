using Dictator.Core;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public interface IPresidentialDecisionMainDialog
    {
        /// <summary>
        ///     Displays the dialog.
        /// </summary>
        /// <returns>The type of presidential decision that has been selected.</returns>
        public DecisionType Show();
    }
}
