using System.Collections.Generic;

namespace Antiboilerplate.String
{
    public static class EnumerableExtensions
    {
        public static string AggregateWithDelimiter(this IEnumerable<string> @this, string delimiter)
            => string.Join(delimiter, @this);
    }
}