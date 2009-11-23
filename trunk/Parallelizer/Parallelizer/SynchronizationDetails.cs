using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Parallelizer
{
    public class SynchronizationDetails<T>
    {
        public AsyncCallbackSynchronizer<T> AsyncCallbackSynchronizer { get; set; }
        public Func<AsyncCallback, T, IAsyncResult> Begin { get; set; }
        public T Argument { get; set; }

        public static IAsyncResult Execute(SynchronizationDetails<T> synchronizationDetails)
        {
            return synchronizationDetails.AsyncCallbackSynchronizer.Execute(
                synchronizationDetails.Begin,
                synchronizationDetails.Argument);
        }
    }
}