using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Parallelizer
{
    public class SequentialMethod<T, TResult>: IMethod<T, TResult>
    {
        void IMethod<T, TResult>.Enqueue(Func<T, TResult> method, T argument)
        {
            
        }
    }
}
