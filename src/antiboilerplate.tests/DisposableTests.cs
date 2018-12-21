using System;
using Antiboilerplate.Disposables;
using FluentAssertions;
using Xunit;

namespace antiboilerplate.tests
{
    public class DisposableTests
    {
        [Fact]
        public void SingleDisposable()
        {
            Disposable.Using(() => new MyDisposable(), d => d).IsDisposed.Should().BeTrue();
        }

        [Fact]
        public void DoubleDisposable()
        {
            Disposable.Using(() => new MyDisposable(), d => new MyDisposable(d), d => d).IsDisposed.Should().BeTrue();
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
