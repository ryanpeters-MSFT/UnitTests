namespace UnitTests.Common.Repositories
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders(Guid clientId);
    }
}
