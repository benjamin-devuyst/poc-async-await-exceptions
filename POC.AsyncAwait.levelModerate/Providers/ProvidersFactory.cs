using System.Collections.Generic;

namespace POC.AsyncAwait.levelModerate
{
    internal sealed class ProvidersFactory
    {
        public IReadOnlyCollection<IProvider> GetProviders()
            => new IProvider[]
            {
                new ProviderOk1(),
                new ProviderOk2(),
                new ProviderOk3(),
                new ProviderInError1(),
                new ProviderInError2(),
                new ProviderInError3(),
                new ProviderInError4(),
            };
    }
}
