using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace antiboilerplate.Web
{
    public static class StringExtensions
    {
        [Obsolete("Expects query string keys to be unique. Use ParseQueryString() instead, which does not have this limitation.")]
        public static IDictionary<string, string> ParseQuery(this string @this)
            => Split(@this)
                .Select(q => new KeyValuePair<string, string>(q.First(),
                    WebUtility.UrlDecode(q.Last()))).ToDictionary(q => q.Key, q => q.Value);

        public static IReadOnlyList<KeyValuePair<string, string>> ParseQueryString(this string @this)
            => Split(@this)
                .Select(q => new KeyValuePair<string, string>(WebUtility.UrlDecode(q.First()),
                    WebUtility.UrlDecode(q.Last())))
                .ToList()
                .AsReadOnly();

        private static IEnumerable<string[]> Split(string s)
            => s
                .Substring(s.IndexOf('?') + 1)
                .Split('&')
                .Select(q => q.Split('='));

        public static Uri ToUri(this string @this, UriKind uriKind = UriKind.RelativeOrAbsolute)
            => new Uri(@this, uriKind);
    }
}