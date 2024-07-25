namespace ppedv.PizzaOrderManager.Model
{
    public class Address : Entity
    {
        public string Name1 { get; set; } = string.Empty;
        public string Name2 { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;

        public ICollection<Order> AsDeliver { get; set; } = new HashSet<Order>();
        public ICollection<Order> AsBilling { get; set; } = new HashSet<Order>();
    }
}
