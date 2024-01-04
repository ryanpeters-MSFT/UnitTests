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
        private static ICollection<Client> clients = new List<Client>
        {
            new Client
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                DateOfBirth = new DateTime(1980, 1, 1)
            },
            new Client
            {
                Id = Guid.NewGuid(),
                Name = "Jane Doe",
                DateOfBirth = new DateTime(1985, 1, 1)
            }
        };

        public ICollection<Client> GetClients()
        {
            return clients;
        }
    }
}
