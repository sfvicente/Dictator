using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IAudienceService
    {
        public void Initialise();
        public Audience SelectRandomUnusedAudienceRequest();
        public void AcceptAudienceRequest(Audience audience);
        public void RefuseAudienceRequest(Audience audience);
    }
}
