using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class Decision: EventAction
    {
        public DecisionType Type { get; set; }
    }
}
