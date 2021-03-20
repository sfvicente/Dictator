using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IAudienceStats
    {
        public Audience CurrentAudienceRequest { get; }

        public void Initialise();
        public void SetRandomAudienceRequest();
    }
}
