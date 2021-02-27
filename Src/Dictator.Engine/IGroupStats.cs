using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IGroupStats
    {
        void Initialise();
        public Group[] GetGroups();
    }
}
