using System;
using System.Text;

namespace Antiboilerplate.String
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string @this) => string.IsNullOrEmpty(@this);

        public static bool IsNullOrWhitespace(this string @this) => string.IsNullOrWhiteSpace(@this);

        public static bool HasText(this string str) => !string.IsNullOrWhiteSpace(str);

        public static string ToUtf8Base64(this string @this) => Convert.ToBase64String(Encoding.UTF8.GetBytes(@this));

        public static string FromUtf8Base64(this string @this) => Encoding.UTF8.GetString(Convert.FromBase64String(@this));
    }
}
