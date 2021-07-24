using Dictator.Core;
using System.Collections.Generic;

namespace Dictator.ConsoleInterface.Revolution
{
    public interface IRevolutionAskForHelpDialog
    {
        int Show(Dictionary<int, Group> possibleAllies);
    }
}
