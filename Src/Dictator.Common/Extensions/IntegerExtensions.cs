using System;

namespace Dictator.Common.Extensions
{
    /// <summary>
    ///     Provides extension methods for the <see cref="int" /> type.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        ///     Returns the absolute value of an integer number.
        /// </summary>
        /// <param name="value">A 32-bit signed integer.</param>
        /// <returns>Returns the absolute value of a 32-bit signed integer.</returns>
        public static int Abs(this int value)
        {
            return Math.Abs(value);
        }
    }
}
