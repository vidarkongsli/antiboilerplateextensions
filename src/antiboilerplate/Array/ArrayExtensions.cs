using System.Collections.Generic;
using System.Linq;

namespace Antiboilerplate.Array
{
    public static class ArrayExtensions
    {
        public static void Deconstruct<T>(this IEnumerable<T> @this, out T first, out IEnumerable<T> rest)
        {
            var array = @this.ToArray();
            first = array.FirstOrDefault();
            rest = array.Skip(1);
        }

        public static void Deconstruct<T>(this IEnumerable<T> @this, out T first, out T second, out IEnumerable<T> rest) =>
            (first, (second, rest)) = @this;

        public static void Deconstruct<T>(this IEnumerable<T> @this, out T first, out T second, out T third, out IEnumerable<T> rest) =>
            (first, second, (third, rest)) = @this;
    }
}