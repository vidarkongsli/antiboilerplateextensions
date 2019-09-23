using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace antiboilerplate.Web
{
    public static class StringExtensions
    {
        public static IDictionary<string, string> ParseQuery(this string @this)
        {
            var firstQuestionMark = @this.IndexOf('?');
            return @this
                .Substring(firstQuestionMark + 1)
                .Split('&')
                .Select(q => q.Split('='))
                .Select(q => new KeyValuePair<string, string>(q.First(), WebUtility.UrlDecode(q.Last())))
                .ToDictionary(q => q.Key, q => q.Value);
        }

        public static Uri ToUri(this string @this, UriKind uriKind = UriKind.RelativeOrAbsolute)
            => new Uri(@this, uriKind);
    }
}