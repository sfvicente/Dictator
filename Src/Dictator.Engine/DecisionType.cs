using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    /// <summary>
    ///     Specifies the type of a presidential decision.
    /// </summary>
    public enum DecisionType
    {
        None = 0,
        PleaseAGroup = 1,
        PleaseAllGroups = 2,
        ImproveYourChanges = 3,
        RaiseSomeCash = 4,
        StrengthenAGroup = 5
    }
}
