using System;
using System.Collections.Generic;

namespace antiboilerplate.Web
{
    public static class UriExtensions
    {
        public static IDictionary<string, string> ParseQuery(this Uri @this) => @this.AbsoluteUri.ParseQuery();
    }
}
