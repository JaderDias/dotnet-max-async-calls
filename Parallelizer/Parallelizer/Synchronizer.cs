using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Parallelizer
{
    public class AsyncCallbackSynchronizer<T>
    {
        AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        IAsyncResult _result;

        public IAsyncResult Execute(Func<AsyncCallback, T, IAsyncResult> beginMethod, T argument)
        {
            beginMethod(Callback, argument);
            _autoResetEvent.WaitOne();
            return _result;
        }

        void Callback(IAsyncResult result)
        {
            _result = result;
            _autoResetEvent.Set();
        }
    }

    public class Synchronizer<T>
    {
        AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        T _result;

        public T Execute(Action<Action<T>> beginMethod)
        {
            beginMethod(Callback);
            _autoResetEvent.WaitOne();
            return _result;
        }

        void Callback(T result)
        {
            _result = result;
            _autoResetEvent.Set();
        }
    }
}
