using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Engine
{
    public class Decision: EventAction
    {
        public DecisionType Type { get; set; }
    }
}
