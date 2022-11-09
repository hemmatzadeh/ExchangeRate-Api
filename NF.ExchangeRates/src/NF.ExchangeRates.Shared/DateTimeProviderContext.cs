using System.Collections.Concurrent;

namespace NF.ExchangeRates.Shared
{
    public class DateTimeProviderContext : IDisposable
    {
        private static readonly ThreadLocal<ConcurrentStack<DateTimeProviderContext>> _threadScopeStack
            = new ThreadLocal<ConcurrentStack<DateTimeProviderContext>>(() => new ConcurrentStack<DateTimeProviderContext>());

        public DateTime ContextUtcNow { get; private set; }

        public static DateTimeProviderContext Current
        {
            get
            {
                if (!_threadScopeStack.Value.TryPeek(out var result))
                {
                    return null;
                }

                return result;
            }
        }

        public DateTimeProviderContext(DateTime utcNow)
        {
            ContextUtcNow = utcNow;
            _threadScopeStack.Value.Push(this);
        }

        public void Dispose()
        {
            _threadScopeStack.Value.TryPop(out var _);
        }
    }
}
