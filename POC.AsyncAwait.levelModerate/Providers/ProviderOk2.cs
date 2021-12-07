using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    /// <summary>
    /// Method is used as a Task{Payload} factory method (async is not defined).
    /// It returns a payload with a Task.FromResult
    /// </summary>
    internal sealed class ProviderOk2 : IProvider
    {
        public Task<Payload> GetValueAsync()
        {
            return Task.FromResult(new Payload(nameof(ProviderOk2)));
        }
    }
}
