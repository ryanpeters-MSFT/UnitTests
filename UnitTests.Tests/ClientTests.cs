using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTests.Common;
using UnitTests.Common.Repositories;
using UnitTests.Services;

namespace UnitTests.Tests
{
    public class ClientTests
    {
        [Fact]
        public void SetClientVipIfEligible()
        {
            var containerFactory = new MockContainerFactory();
            var clientRepositoryMock = new Mock<IClientRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();

            var clientId = Guid.NewGuid();

            // setup/mock the GetClients method to apply to our specific test
            clientRepositoryMock
                .Setup(r => r.GetClient(clientId))
                .Returns(new Client
                {
                    Id = clientId,
                    Name = "John Doe",
                    DateOfBirth = DateTime.Parse("11/20/1983"),
                    IsVip = false
                });

            orderRepositoryMock
                .Setup(r => r.GetOrders(clientId))
                .Returns(new Order[]
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        ClientId = clientId,
                        Created = DateTime.Now.AddDays(-4),
                        TotalCost = 130
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        ClientId = clientId,
                        Created = DateTime.Now.AddDays(-30),
                        TotalCost = 80
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        ClientId = clientId,
                        Created = DateTime.Now.AddMonths(-2),
                        TotalCost = 30
                    }
                });

            // add the default and test-specific mocked services to the collection
            containerFactory
                .ConfigureServices(DefaultServices)
                .ConfigureServices(services =>
                {
                    services.AddTransient(s => clientRepositoryMock.Object);
                    services.AddTransient(s => orderRepositoryMock.Object);
                });

            containerFactory.Invoke(serviceProvider =>
            {
                var clientService = serviceProvider.GetService<ClientService>();

                clientService.UpdateVipStatus(clientId);

                var client = clientService.GetClient(clientId);

                Assert.True(client.IsVip);
            });
        }

        private void DefaultServices(ServiceCollection serviceCollection)
        {
            // common services across all tests
            serviceCollection.AddTransient<ClientService>();
            serviceCollection.AddSingleton(typeof(ILogger<>), typeof(MockLogger<>));
        }
    }
}