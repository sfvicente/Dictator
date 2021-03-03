﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IDecisionStats
    {
        public void Initialise();
        public Decision[] GetDecisionsByType(DecisionType decisionType);
        public void MarkDecisionAsUsed(string text);
    }
}
