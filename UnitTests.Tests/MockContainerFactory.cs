using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Tests
{
    public class MockContainerFactory
    {
        private readonly ServiceCollection _serviceCollection;

        public MockContainerFactory()
        {
            _serviceCollection = new ServiceCollection();
        }

        public MockContainerFactory ConfigureServices(Action<ServiceCollection> serviceCollectionConfig)
        {
            serviceCollectionConfig.Invoke(_serviceCollection);

            return this;
        }

        public void Invoke(Action<ServiceProvider> serviceProviderConfig)
        {
            var serviceProvider = _serviceCollection.BuildServiceProvider();

            serviceProviderConfig.Invoke(serviceProvider);
        }
    }
}