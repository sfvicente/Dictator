namespace Dictator.Core.Services
{
    public interface IAudienceService
    {
        /// <summary>
        ///     Initialises the audience data.
        /// </summary>
        public void Initialise();

        /// <summary>
        ///     Retrieves an unused audience request from the list of audience requests and
        ///     marks it as used.
        /// </summary>
        /// <returns>An unused audience request.</returns>
        public Audience SelectRandomUnusedAudienceRequest();

        /// <summary>
        ///     Accepts an audience request with the associated modifications to group popularity and strength and also the changes to the treasury.
        /// </summary>
        /// <param name="audience">The audience to be accepted.</param>
        public void AcceptAudienceRequest(Audience audience);

        /// <summary>
        ///     Refuses the audience request, resulting in a decrease of popularity with the petitioners.
        /// </summary>
        /// <param name="audience">The audience to be accepted.</param>
        public void RefuseAudienceRequest(Audience audience);
    }
}
