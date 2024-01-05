﻿using Microsoft.Extensions.Logging;
using UnitTests.Common;
using UnitTests.Common.Repositories;

namespace UnitTests.Services
{
    public class ClientService
    {
        private readonly ILogger<ClientService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;

        public ClientService(ILogger<ClientService> logger, IOrderRepository orderRepository, IClientRepository clientRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
        }

        public ICollection<Client> GetClients() => _clientRepository.GetClients();
        public Client GetClient(Guid id) => _clientRepository.GetClient(id);

        /// <summary>
        /// Set a client to VIP status if they purchased >=$200 in the past month
        /// </summary>
        /// <param name="clientId"></param>
        public void UpdateVipStatus(Guid clientId)
        {
            _logger.LogInformation($"Updating client VIP with ID {clientId}");

            try
            {
                var orders = _orderRepository.GetOrders(clientId);

                var lastMonth = DateTime.Now.AddMonths(-1).Date;
                var eligibleOrdersTotal = orders.Where(o => o.Created > lastMonth).Sum(o => o.TotalCost);

                _logger.LogInformation($"Client ID {clientId} has ${eligibleOrdersTotal} total orders");

                if (eligibleOrdersTotal >= 200)
                {
                    var client = _clientRepository.GetClient(clientId);

                    client.IsVip = true;

                    _clientRepository.SaveClient(client);

                    _logger.LogInformation($"Saved client with ID {clientId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update client VIP with ID {clientId}");
            }
        }
    }
}
