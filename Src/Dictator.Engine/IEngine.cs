using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IEngine
    {
        public void Initialise();
        public void AdvanceMonth();
    }
}
