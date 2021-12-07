using System;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    class Program
    {
        /*
         * All developper use TPL (Task Process Library) when they use async or call Task returned method.
         * It is almost easy to do it ... but what if you do not specify async and this code throws an exception...
         * 
         * Well... no task will be returned (no async, nor TaskCompletionSource task managed)...
         * 
         * So you can have exception in code you do not imagine ...
         * 
         * Async Keyword do more than just allowing you to await code. It says to TPL : well just encapsulate my method in a Task and, manage set result with my method returns, or the exception in it.
         * If you miss it in a method that can Throw an exception... It will be a firework
         * 
         * If you miss the async keyword, YOU HAVE TO RETURN A TASK AND NEVER AN EXCEPTION ! 
         * To do it you can use 
         * - a TaskCompletionSource to control the result...
         * - Task.FromResult(...)
         * - Task.FromException(...)
         * - Task.FromCanceled(...)
         * - an instanciated Task
         * - the return of Task.Run or Task.StartNew
         * 
         *   >>>AND NOTHING ELSE !!!<<<
         *   
         * this sample demonstrate that... take it, execute in debug both strategies... and compare the callstacks
         * 
         * Enjoy !
         */

        static async Task Main(string[] args)
        {
            
            var providersFactory = new ProvidersFactory();
            var providers = providersFactory.GetProviders();

            if (args.Contains("-bad"))
                await new AlgoBadPracticeWithTaskStrategy(input => DoSomethingWithTask(input.providerType, input.executedTask)).ExecuteAsync(providers);
            else
                await new AlgoGoodPracticeWithTaskStrategy(input => DoSomethingWithTask(input.providerType, input.executedTask)).ExecuteAsync(providers); ;

            Console.ReadKey();
        }

        private static void DoSomethingWithTask(Type provType, Task<Payload> task)
        {
            if (task.IsCanceled)
            {
                Console.WriteLine($"{provType.Name} - Canceled");
                return;
            }

            if (task.IsFaulted)
            {
                // to avoid TaskScheduler.UnobservedTaskException in Winform or WPF application (application with SyncContext defined)
                task.Exception.Handle((e) => true);

                Console.WriteLine($"{provType.Name} - KO - {task.Exception.Message}");
                return;
            }

            var result = task.Result;
            Console.WriteLine($"{provType.Name} - OK - {result.Origin}");
        }
    }
}
