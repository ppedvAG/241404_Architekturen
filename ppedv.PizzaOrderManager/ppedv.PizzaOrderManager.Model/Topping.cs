namespace ppedv.PizzaOrderManager.Model
{
    public class Topping : Entity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ICollection<Pizza> Pizzas { get; set; } = new HashSet<Pizza>();
    }
}
