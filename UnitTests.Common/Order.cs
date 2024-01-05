namespace UnitTests.Common
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
