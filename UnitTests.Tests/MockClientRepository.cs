using UnitTests.Common;
using UnitTests.Common.Repositories;

namespace UnitTests.Tests
{
    internal class MockClientRepository : IClientRepository
    {
        public ICollection<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    DateOfBirth = new DateTime(1980, 1, 1)
                }
            };
        }
    }
}
