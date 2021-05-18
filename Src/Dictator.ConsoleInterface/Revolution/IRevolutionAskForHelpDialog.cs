using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.Revolution
{
    public interface IRevolutionAskForHelpDialog
    {
        int Show(Dictionary<int, Group> possibleAllies);
    }
}
