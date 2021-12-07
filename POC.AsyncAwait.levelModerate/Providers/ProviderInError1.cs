using System;
using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    /// <summary>
    /// Method is used as a Task{Payload} factory method and (no async defined), the Task result is set in faulted mode (good way)
    /// The usage of Task.FromXXX when async is not defined is a good practice !
    /// </summary>
    internal sealed class ProviderInError1 : IProvider
    {
        public Task<Payload> GetValueAsync()
        {
            return Task.FromException<Payload>(new Exception($"Bwaaa from {nameof(ProviderInError1)}"));
        }
    }
}
