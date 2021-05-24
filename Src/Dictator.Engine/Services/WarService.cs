using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class WarService : IWarService
    {

        /// <summary>
        ///     Determines if a war between the Ritimba republic and Leftoto should happen. There is a 1/3 probability that war should happen.
        /// </summary>
        /// <returns><c>true</c> if war should happen; otherwise, <c>false</c>.</returns>
        public bool ShouldWarHappen()
        {
            Random random = new Random();
            int number = random.Next(0, 3);

            if (number == 0)
            {
                return true;
            }

            return false;
        }
    }
}
