﻿// AsyncActionWrapper.cs

namespace PoshWinRT
{
    using System;
    using System.Threading.Tasks;
    using Windows.Foundation;

    public class AsyncActionWrapper : AsyncInfoWrapper<IAsyncAction>
    {
        public AsyncActionWrapper(object asyncAction) : base(asyncAction) { }

        public void AwaitResult()
        {
            AwaitResult(-1);
        }

        public void AwaitResult(int millisecondsTimeout)
        {
            var task = _asyncInfo.AsTask();
            task.Wait(millisecondsTimeout);

            if (task.IsCompleted)
            {
                return;
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
