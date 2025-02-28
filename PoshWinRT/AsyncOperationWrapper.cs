// AsyncOperationWrapper.cs

namespace PoshWinRT
{
    using System;
    using System.Threading.Tasks;
    using Windows.Foundation;

    public class AsyncOperationWrapper<T> : AsyncInfoWrapper<IAsyncOperation<T>>
    {
        public AsyncOperationWrapper(object asyncOperation) : base(asyncOperation) { }

        public object AwaitResult()
        {
            return AwaitResult(-1);
        }

        public object AwaitResult(int millisecondsTimeout)
        {
            var task = _asyncInfo.AsTask();
            task.Wait(millisecondsTimeout);

            if (task.IsCompleted)
            {
                return task.Result;
            }
            else if (task.IsFaulted)
            {
                throw task.Exception;
            }
            else
            {
                throw new TaskCanceledException(task);
            }
        }
    }
}
