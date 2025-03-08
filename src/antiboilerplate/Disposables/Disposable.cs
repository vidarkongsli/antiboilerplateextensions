using System;
using System.Threading.Tasks;

namespace Antiboilerplate.Disposables
{
    public static class Disposable
    {
        public static TOut Using<TDisposable, TOut>(Func<TDisposable> disposableProducer, Func<TDisposable, TOut> map)
            where TDisposable : IDisposable
        {
            using (var x = disposableProducer())
            {
                return map(x);
            }
        }

        public static async Task<TOut> Using<TDisposable, TOut>(Func<TDisposable> disposableProducer, Func<TDisposable, Task<TOut>> map)
            where TDisposable : IDisposable
        {
            using (var x = disposableProducer())
            {
                return await map(x);
            }
        }

        public static TOut Using<TDisposable1, TDisposable2, TOut>(Func<TDisposable1> disposableProducer1,
            Func<TDisposable1, TDisposable2> disposableProducer2, Func<TDisposable2, TOut> map)
            where TDisposable1 : IDisposable where TDisposable2 : IDisposable
        {
            using (var x = disposableProducer1())
            using (var y = disposableProducer2(x))
            {
                return map(y);
            }
        }

        public static async Task<TOut> Using<TDisposable1, TDisposable2, TOut>(Func<TDisposable1> disposableProducer1,
            Func<TDisposable1, TDisposable2> disposableProducer2, Func<TDisposable2, Task<TOut>> map)
            where TDisposable1 : IDisposable where TDisposable2 : IDisposable
        {
            using (var x = disposableProducer1())
            using (var y = disposableProducer2(x))
            {
                return await map(y);
            }
        }

        public static TOut Using<TDisposable1, TDisposable2, TOut>(Func<TDisposable1> disposableProducer1,
            Func<TDisposable1, TDisposable2> disposableProducer2, Func<TDisposable1, TDisposable2, TOut> map)
            where TDisposable1 : IDisposable where TDisposable2 : IDisposable
        {
            using (var x = disposableProducer1())
            using (var y = disposableProducer2(x))
            {
                return map(x, y);
            }
        }

        public static async Task<TOut> Using<TDisposable1, TDisposable2, TOut>(Func<TDisposable1> disposableProducer1,
            Func<TDisposable1, TDisposable2> disposableProducer2, Func<TDisposable1, TDisposable2, Task<TOut>> map)
            where TDisposable1 : IDisposable where TDisposable2 : IDisposable
        {
            using (var x = disposableProducer1())
            using (var y = disposableProducer2(x))
            {
                return await map(x, y);
            }
        }
    }
}
