using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Antiboilerplate.Functional;

public static class FunctionalExtensions
{
    [DebuggerStepThrough]
    public static TOut Map<TIn, TOut>(this TIn @this, Func<TIn, TOut> func)
        => @this == null ? default : func(@this);

    [DebuggerStepThrough]
    public static T Then<T>(this T @this, Action<T> then)
    {
        if (@this != null) then(@this);
        return @this;
    }

    [DebuggerStepThrough]
    public static ICollection<T> AsCollection<T>(this T @this) where T : class
        => @this switch
        {
            null => [],
            ICollection<T> coll => coll,
            _ => [@this]
        };

    [DebuggerStepThrough]
    public static void Each<T>(this IEnumerable<T> @this, Action<T> action)
    {
        if (@this == null) return;
        foreach (var element in @this)
        {
            action(element);
        }
    }

    [DebuggerStepThrough]
    public static void Each<T>(this IEnumerable<T> @this, Action<T, long> action)
    {
        if (@this == null) return;
        long i = 0;
        foreach (var element in @this)
        {
            action(element, i++);
        }
    }
}