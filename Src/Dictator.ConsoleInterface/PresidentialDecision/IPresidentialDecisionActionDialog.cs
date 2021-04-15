﻿using Dictator.ConsoleInterface.Common;
using Dictator.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.ConsoleInterface.PresidentialDecision
{
    public interface IPresidentialDecisionActionDialog
    {
        DialogResult Show(Decision decision);
    }
}
