﻿using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public interface IPresidentialDecisionSubDialog
    {
        public int Show(DecisionType decisionType);
    }
}
