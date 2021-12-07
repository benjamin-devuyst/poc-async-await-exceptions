using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    /// <summary>
    /// Method is used as a Task{Payload} factory method (async is not defined).
    /// It use a TaskCompletionSource based on another async method (with a Task.Delay)
    /// </summary>
    internal sealed class ProviderOk3 : IProvider
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
            return new Payload(nameof(ProviderOk3));
        }
    }
}
