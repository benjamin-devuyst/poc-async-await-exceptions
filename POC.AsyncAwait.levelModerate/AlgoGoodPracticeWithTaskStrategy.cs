using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    /// <summary>
    /// The call of GetValueAsync is protected in a Try Catch. So every scenario can be handled.
    /// </summary>
    internal sealed class AlgoGoodPracticeWithTaskStrategy
    {
        private readonly Action<(Type providerType, Task<Payload> executedTask)> postExecutionTask;

        public AlgoGoodPracticeWithTaskStrategy(Action<(Type providerType, Task<Payload> executedTask)> postExecutionTask)
        {
            this.postExecutionTask = postExecutionTask;
        }

        public async Task ExecuteAsync(IReadOnlyCollection<IProvider> providers)
        {
            List<Task> awaitedTasks = new List<Task>();

            foreach (var prov in providers)
            {
                var task = GetPayloadFromProviderAsync(prov);
                var waitedTask = task.ContinueWith(t => postExecutionTask((prov.GetType(), t)));
                awaitedTasks.Add(waitedTask);
            }

            // await that all continuewith to DoSomethingWithTask has been executed
            await Task.WhenAll(awaitedTasks);
        }

        private static Task<Payload> GetPayloadFromProviderAsync(IProvider provider) 
            => TaskExtensions.ExecuteInSandbox(provider.GetValueAsync);

    }
}
