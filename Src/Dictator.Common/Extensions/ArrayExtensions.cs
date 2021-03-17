using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Common.Extensions
{
    public static class ArrayExtensions
    {
        public static bool IsEmpty<T>(this T[] array)
        {
            return array.Length == 0;
        }
    }
}
