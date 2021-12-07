using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    /// <summary>
    /// Bad ! the call of GetValueAsync is not between a try catch approach (async defined or not).
    /// </summary>
    internal sealed class AlgoBadPracticeWithTaskStrategy
    {
        private readonly Action<(Type providerType, Task<Payload> executedTask)> postExecutionTask;

        public AlgoBadPracticeWithTaskStrategy(Action<(Type providerType, Task<Payload> executedTask)> postExecutionTask)
        {
            this.postExecutionTask = postExecutionTask;
        }

        public async Task ExecuteAsync(IReadOnlyCollection<IProvider> providers)
        {
            List<Task> awaitedTasks = new List<Task>();

            foreach (var prov in providers)
            {
                Console.WriteLine($"{prov.GetType().Name} - Get Task");

                // it is here that problem can occurs... if GetValueAsync has no async and do not use Task.FromException
                var task = prov.GetValueAsync();

                var waitedTask = task.ContinueWith(t => postExecutionTask((prov.GetType(), t)));
                awaitedTasks.Add(waitedTask);
            }

            Console.WriteLine($"AWAIT tasks");
            // await that all continuewith to DoSomethingWithTask has been executed
            await Task.WhenAll(awaitedTasks);
        }
    }
}
