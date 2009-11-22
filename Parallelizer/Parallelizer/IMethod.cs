using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parallelizer
{
    public interface IMethod<T, TResult>
    {
        void Enqueue(Func<T, TResult> method, T argument);
    }
}
