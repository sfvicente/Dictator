using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IAudienceService
    {
        public void Initialise();
        public Audience SelectRandomUnusedAudienceRequest();
    }
}
