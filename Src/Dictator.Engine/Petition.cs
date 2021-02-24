using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public class Petition : EventAction
    {
        public GroupType Requester { get; set; }
    }
}
