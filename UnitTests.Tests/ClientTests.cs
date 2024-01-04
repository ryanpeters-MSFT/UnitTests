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
        private readonly ServiceCollection _commonServices;

        public ClientTests()
        {
            _commonServices = new ServiceCollection();

            // common services across all tests
            _commonServices.AddTransient<ClientService>();
            _commonServices.AddSingleton(typeof(ILogger<>), typeof(MockLogger<>));
        }

        [Fact]
        public void GetEligibleClients()
        {
            // configure test-specific services using Moq
            void services(ServiceCollection services)
            {
                // initialize the mock repo
                var clientRepositoryMock = new Mock<IClientRepository>();

                // setup/mock the GetClients method to apply to our specific test
                clientRepositoryMock.Setup(r => r.GetClients()).Returns(new List<Client>
                {
                    new Client { Id = Guid.NewGuid(), Name = "Moq Client", DateOfBirth = DateTime.Parse("11/20/1983") }, // over 18
                    new Client { Id = Guid.NewGuid(), Name = "Minor Client", DateOfBirth = DateTime.Parse("4/13/2010") } // under 18
                });

                // add the mocked service to the collection
                services.AddTransient(s => clientRepositoryMock.Object);
            }

            Invoke(services, serviceProvider =>
            {
                var clientService = serviceProvider.GetService<ClientService>();

                var clients = clientService.GetEligibleClients();

                Assert.NotEmpty(clients);
                Assert.Equal(1, clients.Count);
            });
        }

        #region Move to base class

        private void Invoke(Action<ServiceCollection> services, Action<ServiceProvider> action)
        {
            services.Invoke(_commonServices);

            Invoke(action);
        }

        private void Invoke(Action<ServiceProvider> action)
        {
            var serviceProvider = _commonServices.BuildServiceProvider();

            action.Invoke(serviceProvider);
        }

        #endregion
    }
}