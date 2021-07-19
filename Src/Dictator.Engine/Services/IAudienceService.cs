using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IAudienceService
    {
        public void Initialise();

        /// <summary>
        ///     Retrieves an unused audience request from the list of audience requests and
        ///     marks it as used.
        /// </summary>
        /// <returns>An unused audience request.</returns>
        public Audience SelectRandomUnusedAudienceRequest();

        public void AcceptAudienceRequest(Audience audience);
        public void RefuseAudienceRequest(Audience audience);
    }
}
