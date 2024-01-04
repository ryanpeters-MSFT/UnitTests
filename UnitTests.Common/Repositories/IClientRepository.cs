namespace UnitTests.Common.Repositories
{
    public interface IClientRepository
    {
        ICollection<Client> GetClients();
    }
}
