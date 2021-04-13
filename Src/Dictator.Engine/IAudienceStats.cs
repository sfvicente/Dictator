using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IAudienceStats
    {
        public void Initialise();
        public IEnumerable<Audience> GetUnusedAudiences();
    }
}
