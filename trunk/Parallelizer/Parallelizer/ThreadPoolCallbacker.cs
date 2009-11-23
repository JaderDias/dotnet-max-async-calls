using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Parallelizer
{
    public class ThreadPoolCallbacker<T, TResult>
    {
        List<TResult> _results = new List<TResult>();
        int _enqueuedItemCount = 0;
        Action<IEnumerable<TResult>> _callback;
        Func<T, TResult> _func;

        public ThreadPoolCallbacker(Func<T, TResult> func)
        {
            _func = func;
        }

        public void Queue(T argument)
        {
            Interlocked.Increment(ref _enqueuedItemCount);
            ThreadPool.QueueUserWorkItem(ItemCallback, argument);
        }

        public void EnableCallback(Action<IEnumerable<TResult>> callback)
        {
            lock (_results)
            {
                _callback = callback;
                TryCallback();
            }
        }

        void TryCallback()
        {
            if (!ReferenceEquals(null, _callback) &&
                _results.Count == _enqueuedItemCount)
            {
                _callback(_results);
            }
        }

        void ItemCallback(object state)
        {
            var result = _func((T)state);
            lock (_results)
            {
                _results.Add(result);
                TryCallback();
            }
        }
    }
}
