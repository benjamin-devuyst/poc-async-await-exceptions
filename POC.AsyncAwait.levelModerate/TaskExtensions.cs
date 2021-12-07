using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    internal static class TaskExtensions
    {
        public static void TrySynchronizeTasks<TResult>(this Task<TResult> source, TaskCompletionSource<TResult> target)
        {
            if (source.IsFaulted)
                target.TrySetException(source.Exception);
            else if (source.IsCanceled)
                target.TrySetCanceled();
            else
                target.TrySetResult(source.Result);
        }

        public static Task<TResult> ExecuteInSandbox<TResult>(this Func<Task<TResult>> getTaskSourceFactory)
        {
            var tcs = new TaskCompletionSource<TResult>();
            Task<TResult> realTask = null;

            // catch les exceptions hors async et hors Task.FromException
            try { realTask = getTaskSourceFactory(); }
            catch (Exception exc) { tcs.SetException(exc); }

            realTask?.ContinueWith(t => t.TrySynchronizeTasks(tcs));

            return tcs.Task;
        }
    }
}
