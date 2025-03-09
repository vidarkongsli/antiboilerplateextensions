using System;
using System.Collections.Generic;

namespace antiboilerplate.Web
{
    public static class UriExtensions
    {
        [Obsolete("Expects query string keys to be unique. Use ParseQueryString() instead, which does not have this limitation.")]
        public static IDictionary<string, string> ParseQuery(this Uri @this) => @this.AbsoluteUri.ParseQuery();

        public static IReadOnlyList<KeyValuePair<string, string>> ParseQueryString(this Uri @this)
            => @this.AbsoluteUri.ParseQueryString();

    }
}
