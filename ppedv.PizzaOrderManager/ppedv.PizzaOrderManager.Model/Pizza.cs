namespace ppedv.PizzaOrderManager.Model
{
    public class Pizza : Entity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public PizzaSize Size { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public ICollection<Topping> Toppings { get; set; } = new HashSet<Topping>();

    }
}
