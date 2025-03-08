using System;
using System.Threading.Tasks;
using Antiboilerplate.Disposables;
using FluentAssertions;
using Xunit;

namespace Antiboilerplate.Tests
{
    public class DisposableTests
    {
        [Fact]
        public void SingleDisposable()
        {
            Disposable.Using(() => new MyDisposable(), d => d).IsDisposed.Should().BeTrue();
        }

        [Fact]
        public async Task SingleDisposableAsync()
        {
            (await Disposable.Using(() => new MyDisposable(), Task.FromResult)).IsDisposed.Should().BeTrue();
        }

        [Fact]
        public void DoubleDisposable()
        {
            Disposable.Using(() => new MyDisposable(), d => new MyDisposable(d), d => d).IsDisposed.Should().BeTrue();
        }

        [Fact]
        public async Task DoubleDisposableAsync()
        {
            (await Disposable.Using(() => new MyDisposable(), d => new MyDisposable(d), Task.FromResult)).IsDisposed.Should().BeTrue();
        }

        [Fact]
        public void DoubleDisposableBothPassed()
        {
            var (d1, d2) = Disposable.Using(() => new MyDisposable(), d => new MyDisposable(d), (d1,d2) => (d2,d2));
            d1.IsDisposed.Should().BeTrue();
            d2.IsDisposed.Should().BeTrue();
        }
        [Fact]
        public async Task DoubleDisposableBothPassedAsync()
        {
            var (d1, d2) = await Disposable.Using(() => new MyDisposable(), d => new MyDisposable(d),
                (d1, d2) => Task.FromResult((d2, d2)));
            d1.IsDisposed.Should().BeTrue();
            d2.IsDisposed.Should().BeTrue();
        }

        internal class MyDisposable : IDisposable
        {
            private readonly MyDisposable _inner;

            public MyDisposable()
            {
            }

            public MyDisposable(MyDisposable inner)
            {
                _inner = inner;
            }

            private bool _isDisposed;

            public bool IsDisposed => _isDisposed && (_inner?.IsDisposed ?? true);

            public void Dispose()
            {
                _isDisposed = true;
            }
        }
    }
}
