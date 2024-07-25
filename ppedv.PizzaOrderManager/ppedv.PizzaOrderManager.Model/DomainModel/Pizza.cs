namespace ppedv.PizzaOrderManager.Model.DomainModel
{
    public class Pizza : Entity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public PizzaSize Size { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public virtual ICollection<Topping> Toppings { get; set; } = new HashSet<Topping>();

    }
}
