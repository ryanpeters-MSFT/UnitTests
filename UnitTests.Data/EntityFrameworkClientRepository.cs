using UnitTests.Common;
using UnitTests.Common.Repositories;

namespace UnitTests.Data
{
    /// <summary>
    /// Not REALLY an EF repo, but very well could be in production
    /// </summary>
    public class EntityFrameworkClientRepository : IClientRepository
    {
        // mock a list of Client objects
        private static readonly ICollection<Client> clients = new List<Client>
        {
            new() 
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                DateOfBirth = new DateTime(1980, 1, 1)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Jane Doe",
                DateOfBirth = new DateTime(1985, 1, 1)
            }
        };

        public Client GetClient(Guid id)
        {
            return clients.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Client> GetClients() => clients;

        public void SaveClient(Client client)
        {
            var existingClient = clients.FirstOrDefault(c => c.Id == client.Id);

            if (existingClient != null)
            {
                existingClient.Name = client.Name;
                existingClient.IsVip = client.IsVip;
                existingClient.DateOfBirth = client.DateOfBirth;
            }
        }
    }
}
