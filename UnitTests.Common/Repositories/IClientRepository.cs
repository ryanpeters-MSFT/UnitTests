namespace UnitTests.Common.Repositories
{
    public interface IClientRepository
    {
        Client GetClient(Guid id);
        ICollection<Client> GetClients();
        void SaveClient(Client client);
    }
}
