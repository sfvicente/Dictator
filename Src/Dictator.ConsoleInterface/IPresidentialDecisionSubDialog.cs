using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface
{
    public interface IPresidentialDecisionSubDialog
    {
        public int Show(DecisionType decisionType);
    }
}
