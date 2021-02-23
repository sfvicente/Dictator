using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Engine
{
    public class Petition : EventAction
    {
        public GroupType Requester { get; set; }
    }
}
