using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Antiboilerplate.Functional
{
    public static class FunctionalExtensions
    {
        [DebuggerStepThrough]
        public static TOut Map<TIn, TOut>(this TIn @this, Func<TIn, TOut> func)
            => @this == null ? default(TOut) : func(@this);

        [DebuggerStepThrough]
        public static T Then<T>(this T @this, Action<T> then)
        {
            if (@this != null) then(@this);
            return @this;
        }

        [DebuggerStepThrough]
        public static ICollection<T> AsCollection<T>(this T @this) where T : class
            => @this == null ? new T[0] : new[] { @this };

        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> @this, Action<T> action)
        {
            if (@this == null) return;
            foreach (var element in @this)
            {
                action(element);
            }
        }
    }
}
