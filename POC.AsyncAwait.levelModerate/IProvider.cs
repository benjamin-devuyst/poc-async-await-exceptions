using System.Threading.Tasks;

namespace POC.AsyncAwait.levelModerate
{
    internal interface IProvider
    {
        Task<Payload> GetValueAsync();
    }
}
