using Microsoft.Extensions.Logging;
using UnitTests.Common;
using UnitTests.Common.Repositories;

namespace UnitTests.Services
{
    public class ClientService
    {
        private readonly ILogger<ClientService> _logger;
        private readonly IClientRepository _clientRepository;

        public ClientService(ILogger<ClientService> logger, IClientRepository clientRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
        }

        public ICollection<Client> GetClients()
        {
            _logger.LogInformation("Getting clients");

            var clients = _clientRepository.GetClients();

            _logger.LogInformation("Got {Count} clients", clients.Count);

            return clients;
        }

        public ICollection<Client> GetEligibleClients()
        {
            _logger.LogInformation("Getting clients over the age of 18");
            
            DateTime date = DateTime.Now.Date;

            var clients = _clientRepository.GetClients().Where(c => c.DateOfBirth.Year <= date.Year - 18).ToList();

            _logger.LogInformation("Got {Count} eligible clients", clients.Count);

            return clients;
        }
    }
}
