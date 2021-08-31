namespace Dictator.Core.Services
{
    /// <summary>
    ///     Provides functionality related to the player assassination mechanic.
    /// </summary>
    public interface IAssassinationService
    {
        /// <summary>
        ///     Determines if an assassination attempt on the player is successful.
        /// </summary>
        /// <returns><c>true</c> if the assassination atempt is successful; otherwise, <c>false</c>.</returns>
        public bool IsAssassinationSuccessful();

        /// <summary>
        ///     Retrieves the name of the group responsible for the assassination attempt.
        /// </summary>
        /// <returns>The name of the assassination group.</returns>
        public string GetAssassinationGroupName();
    }
}
