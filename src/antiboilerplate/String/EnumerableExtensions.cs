using System.Collections.Generic;
using System.Linq;

namespace Antiboilerplate.String
{
    public static class EnumerableExtensions
    {
        public static string AggregateWithDelimiter(this IEnumerable<string> @this, string delimiter)
            => string.Join(delimiter, @this);

        public static IDictionary<TKey, TValue[]> ToDictionaryOfArrays<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> list)
         => list.GroupBy(x => x.Key)
                .ToDictionary(g => g.Key,
                    g => g.Select(p => p.Value).ToArray());
    }
}