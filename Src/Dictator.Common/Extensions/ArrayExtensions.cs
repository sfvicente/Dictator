using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Dictator.Common.Extensions
{
    public static class ArrayExtensions
    {
        public static bool IsEmpty<T>(this T[] array)
        {
            return array.Length == 0;
        }

        public static bool IsNotEmpty<T>(this T[] array)
        {
            return array.Length > 0;
        }

        /// <summary>
        ///     Returns a read-only wrapper for the specified array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based array to wrap in a read-only ReadOnlyCollection<T> wrapper.</param>
        /// <returns>A read-only <c>ReadOnlyCollection<T></c> wrapper for the specified array.</returns>
        public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
        {
            return Array.AsReadOnly(array);
        }
    }
}