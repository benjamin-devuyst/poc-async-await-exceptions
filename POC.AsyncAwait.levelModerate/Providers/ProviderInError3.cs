using System;
using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    /// <summary>
    /// Method is used as a Payload returned method (async is defined). The method throws an exception directly.
    /// So, the presence of async will say to TPL that it must be encapsulated in a Task.
    /// TPL will set the Task as faulted with the exception thrown
    /// The usage of async is a good practice !
    /// </summary>
    internal sealed class ProviderInError3 : IProvider
    {
        public async Task<Payload> GetValueAsync()
        {
            throw new Exception($"Bwaaa from {nameof(ProviderInError3)}");
        }
    }
}
