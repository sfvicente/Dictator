using Dictator.ConsoleInterface.Common;
using Dictator.Core;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public interface IPresidentialDecisionActionDialog
    {
        DialogResult Show(Decision decision);
    }
}
