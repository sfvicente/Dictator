using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IAudienceService
    {
        public void Initialise();
        public IEnumerable<Audience> GetUnusedAudiences();
        public Audience SelectRandomUnusedAudienceRequest();
    }
}
