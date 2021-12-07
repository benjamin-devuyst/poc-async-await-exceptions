using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    /// <summary>
    /// Method is used as a Payload returned method (async is defined).
    /// It returns a payload after a delay.
    /// </summary>
    internal sealed class ProviderOk1 : IProvider
    {
        public async Task<Payload> GetValueAsync()
        {
            await Task.Delay(1500);
            return new Payload(nameof(ProviderOk1));
        }
    }
}
