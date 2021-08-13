using Dictator.Core;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public interface IPresidentialDecisionSubDialog
    {
        public int Show(Decision[] decisions);
    }
}
