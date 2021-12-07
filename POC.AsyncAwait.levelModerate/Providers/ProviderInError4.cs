using System;
using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    /// <summary>
    /// Method is used as a Task{Payload} factory method and (no async defined).
    /// It use a TaskCompletionSource to returns a task.
    /// This one is synchronized with an async method executed (exception in async method will be synchronized with the TaskCompletionSource)
    /// This usage is a good practice because of combining TaskCompletionSource (you have the control on the result you set) with the encapsulation of the business algorithm !
    /// </summary>
    internal sealed class ProviderInError4 : IProvider
    {
        public Task<Payload> GetValueAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<Payload>();
            DoSomethingAsync()
                .ContinueWith(t => t.TrySynchronizeTasks(taskCompletionSource));

            return taskCompletionSource.Task;
        }

        private async Task<Payload> DoSomethingAsync()
        {
            await Task.Delay(2000);
            throw new Exception($"Bwaaa from {nameof(ProviderInError4)}");
        }
    }
}
