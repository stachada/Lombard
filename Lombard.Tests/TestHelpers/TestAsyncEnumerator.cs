using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lombard.Tests
{
    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public T Current => _inner.Current;

        public void Dispose()
        {
            _inner.Dispose();
        }

        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return await Task.FromResult(_inner.MoveNext());
        }
    }
}
