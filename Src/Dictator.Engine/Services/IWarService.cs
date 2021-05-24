using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IWarService
    {
        /// <summary>
        ///     Determines if a war between the Ritimba republic and Leftoto should happen. There is a 1/3 probability that war should happen.
        /// </summary>
        /// <returns><c>true</c> if war should happen; otherwise, <c>false</c>.</returns>
        public bool ShouldWarHappen();
    }
}
