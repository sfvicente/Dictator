using Dictator.Core;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player is given the list of sub-options
    ///     within a specific option to make a presidential decision.
    /// </summary>
    public interface IPresidentialDecisionSubDialog
    {
        public int Show(Decision[] decisions);
    }
}
