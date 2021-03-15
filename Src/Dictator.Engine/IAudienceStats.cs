using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    public interface IAudienceStats
    {
        Audience CurrentAudienceRequest { get; }

        void Initialise();
        void SetRandomAudienceRequest();
    }
}
