using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IAssassinationService
    {
        /// <summary>
        ///     Determines if an assassination attempt on the player is successful.
        /// </summary>
        /// <returns><c>true</c> if the assassination atempt is successful; otherwise, <c>false</c>.</returns>
        public bool IsAssassinationSuccessful();
    }
}
